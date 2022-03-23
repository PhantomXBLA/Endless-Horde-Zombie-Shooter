using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ZombieIdleState : ZombieStates
{
    int movementZHash = Animator.StringToHash("MovementZ");

    public ZombieIdleState(ZombieComponent zombie, StateMachine stateMachine) : base(zombie, stateMachine)
    {

    }
    // Start is called before the first frame update
    public override void Start()
    {
        base.Start();
        ownerZombie.zombieNavMesh.isStopped = true;
        ownerZombie.zombieNavMesh.ResetPath();
        ownerZombie.zombieAnimator.SetFloat(movementZHash, 0);
    }

    public override void Exit()
    {
        base.Exit();
        ownerZombie.zombieNavMesh.isStopped = false;
    }
}
