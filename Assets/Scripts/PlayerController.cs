using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerController : MonoBehaviour
{

    public bool isFiring;
    public bool isReloading;
    public bool isJumping;
    public bool isRunning;
    public bool isAiming;

    public InventoryComponent inventory;

    public int unibeamCharge = 0;

    public List<AudioSource> UnibeamSounds = new List<AudioSource>();

    // Start is called before the first frame update
    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {
        
    }

    public void IncrementUnibeam(int amount)
    {
        unibeamCharge += amount;

        switch (unibeamCharge)
        {
            case 1:
                UnibeamSounds[0].Play();
                break;

            case 2:
                UnibeamSounds[1].Play();
                break;

            case 4:
                UnibeamSounds[2].Play();
                break;
        }
    }
}
