using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ZombieSpawner : MonoBehaviour
{
    public int numberOfZombiesToSpawn;
    public GameObject[] zombiePrefabs;
    public SpawnerVolume[] spawnVolumes;

    GameObject followGameObject;
    // Start is called before the first frame update
    void Start()
    {
        followGameObject = GameObject.FindGameObjectWithTag("Player");

        for (int i = 0; i < numberOfZombiesToSpawn; i++)
        {
            SpawnZombie();
        }
    }


    public void SpawnZombie()
    {
        GameObject zombieToSpawn = zombiePrefabs[Random.Range(0, zombiePrefabs.Length)];
        SpawnerVolume spawnVolume = spawnVolumes[Random.Range(0, spawnVolumes.Length)];

        if (!followGameObject) return;

        GameObject zombie = Instantiate(zombieToSpawn, spawnVolume.GetPositionInBounds(), spawnVolume.transform.rotation);

        zombie.GetComponent<ZombieComponent>().Initialize(followGameObject);
    }
}
