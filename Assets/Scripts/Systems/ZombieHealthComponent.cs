using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ZombieHealthComponent : HealthComponent
{

    int dropChance = 7;

    ZombieStateMachine zombieStates;

    ZombieSpawner zombieSpawner;

    public GameObject FlightRestore;
    public GameObject UnibeamPower;

    bool canSpawnFlight = true;
    bool canSpawnUni = true;
    bool canSpawnZombie = true;
    private void Awake()
    {
        zombieStates = GetComponent<ZombieStateMachine>();
        zombieSpawner = GameObject.Find("ZombieSpawner").GetComponent<ZombieSpawner>();
    }

    public override void Destroy()
    {
        zombieStates.ChangeState(ZombieStateType.isDead);

        int calculatedDrop = Random.Range(1, 30);

        Debug.Log(calculatedDrop);

        if(calculatedDrop == 1 && canSpawnFlight)
        {
            Instantiate(FlightRestore, new Vector3(this.transform.position.x, this.transform.position.y + 1, this.transform.position.z), Quaternion.identity);
            canSpawnFlight = false;
        }

        int calculatedUnibeamDrop = Random.Range(1, 60);

        if(calculatedUnibeamDrop == 1 && canSpawnUni)
        {
            Instantiate(UnibeamPower, new Vector3(this.transform.position.x, this.transform.position.y + 1, this.transform.position.z), Quaternion.identity);
            canSpawnUni = false;
        }


        int spawnZombieChance = Random.Range(1, 11);

        if(spawnZombieChance >= 9 && canSpawnZombie)
        {
            zombieSpawner.SpawnZombie();
            canSpawnZombie = false;
        }
    }
}
