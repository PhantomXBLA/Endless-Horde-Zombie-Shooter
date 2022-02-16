using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public enum WeaponType
{
    None,
    Pistol,
    MachineGun
}

[System.Serializable]

public struct WeaponStats
{
    public WeaponType weaponType;
    public string weaponName;
    public float damage;
    public int bulletsInMag;
    public int magSize;
    public float fireRate;
    public float fireStartDelay;
    public float fireDistance;
    public bool repeating;

    public LayerMask weaponHitLayers;
}
public class WeaponComponent : MonoBehaviour
{
    public Transform gripLocation;
    protected WeaponHolder weaponHolder;

    [SerializeField] 
    public WeaponStats weaponStats;

    public bool isFiring = false;
    public bool isReloading = false;

    protected Camera mainCamera;


    // Start is called before the first frame update
    void Start()
    {
        
    }

    private void Awake()
    {
        mainCamera = Camera.main;
    }

    // Update is called once per frame
    void Update()
    {
        
    }

    public void Initialize(WeaponHolder wh)
    {
        weaponHolder = wh;
    }

    public virtual void StartFiring()
    {
        isFiring = true;
        if (weaponStats.repeating)
        {
            InvokeRepeating(nameof(Fire), weaponStats.fireStartDelay, weaponStats.fireRate);
        }
        else
        {
            Fire();
        }
    }

    public virtual void StopFiring()
    {
        isFiring = false;
        CancelInvoke(nameof(Fire));
    }

    protected virtual void Fire()
    {
        print("Fire Weapon!");
        weaponStats.bulletsInMag--;
    }
}
