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
    public int reserveAmmo;
    public float fireRate;
    public float fireStartDelay;
    public float fireDistance;
    public bool repeating;

    public LayerMask weaponHitLayers;
}
public class WeaponComponent : MonoBehaviour
{
    public Transform gripLocation;
    public Transform muzzleLocation;

    protected WeaponHolder weaponHolder;

    [SerializeField] 
    protected ParticleSystem firingEffect;

    [SerializeField] 
    public WeaponStats weaponStats;

    public bool isFiring = false;
    public bool isReloading = false;

    protected Camera mainCamera;

    [SerializeField]
    protected AudioSource firingSound;
    protected bool fireSoundCanPlay = true;

    [SerializeField]
    protected AudioSource reloadSound;
    [SerializeField]
    protected AudioSource jarvisReload;

    public LineRenderer lineRendererR;
    public LineRenderer lineRendererL;



    // Start is called before the first frame update
    void Start()
    {
        //firingEffect.transform.parent = muzzleLocation;
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

        firingSound.Stop();
        fireSoundCanPlay = true;

        if (firingEffect.isPlaying)
        {
            firingEffect.Stop();

        }
    }

    protected virtual void Fire()
    {
        print("Fire Weapon!");
        weaponStats.bulletsInMag--;
    }

    public virtual void StartReloading()
    {

        isReloading = true;
        ReloadWeapon();
    }

    public virtual void StopReloading()
    {
        isReloading = false;

    }

    protected virtual void ReloadWeapon()
    {
        reloadSound.Play();

        int jarvisVoicelineChance = 1;

        int jarvisVoicelineRoll = Random.Range(1, 10);

        if(jarvisVoicelineChance == jarvisVoicelineRoll)
        {
            jarvisReload.Play();
        }

       

        if (firingEffect.isPlaying)
        {
            firingEffect.Stop();
        }


        int bulletsToReload = weaponStats.reserveAmmo - (weaponStats.magSize - weaponStats.bulletsInMag);

        if (bulletsToReload > 0)
        {
            weaponStats.reserveAmmo = bulletsToReload;
            weaponStats.bulletsInMag = weaponStats.magSize;

        }
        else
        {
            weaponStats.bulletsInMag += weaponStats.reserveAmmo;
            weaponStats.reserveAmmo = 0;
        }
    }
}
