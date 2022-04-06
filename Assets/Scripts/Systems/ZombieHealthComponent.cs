using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ZombieHealthComponent : HealthComponent
{
    ZombieStateMachine zombieStates;
    private void Awake()
    {
        zombieStates = GetComponent<ZombieStateMachine>();
    }

    public override void Destroy()
    {
        zombieStates.ChangeState(ZombieStateType.isDead);
    }
}
