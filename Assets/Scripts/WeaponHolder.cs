using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class WeaponHolder : MonoBehaviour
{
    // Start is called before the first frame update

    [Header("WeaponToSpawn"), SerializeField]
    GameObject WeaponToSpawn;

    PlayerController playerController;
    Sprite crosshairImage;

    [SerializeField]
    GameObject WeaponSocket;
    void Start()
    {
        GameObject spawnedWeapon = Instantiate(WeaponToSpawn, WeaponSocket.transform.position, WeaponSocket.transform.rotation, WeaponSocket.transform);
    }

    // Update is called once per frame
    void Update()
    {
        
    }
}
