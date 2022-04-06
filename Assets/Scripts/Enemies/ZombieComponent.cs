using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.AI;

public class ZombieComponent : MonoBehaviour
{

    public float zombieDamage = 5;
    public NavMeshAgent zombieNavMesh;
    public Animator zombieAnimator;

    public ZombieStateMachine stateMachine;
    public GameObject followTarget;

    public void Awake()
    {
        zombieAnimator = GetComponent<Animator>();
        zombieNavMesh = GetComponent<NavMeshAgent>();
        stateMachine = GetComponent<ZombieStateMachine>();

        
    }

    private void Start()
    {
        Initialize(followTarget);
    }

    public void Initialize(GameObject _followTarget)
    {
        followTarget = _followTarget;

        ZombieIdleState idleState = new ZombieIdleState(this, stateMachine);
        stateMachine.AddState(ZombieStateType.Idle, idleState);

        ZombieFollowState followState = new ZombieFollowState(followTarget, this, stateMachine);
        stateMachine.AddState(ZombieStateType.Following, followState);

        ZombieAttackState attackState = new ZombieAttackState(followTarget, this, stateMachine);
        stateMachine.AddState(ZombieStateType.Attacking, attackState);

        ZombieDeadState deadState = new ZombieDeadState(this, stateMachine);
        stateMachine.AddState(ZombieStateType.isDead, deadState);

        stateMachine.Initialize(ZombieStateType.Following);
    }
}
