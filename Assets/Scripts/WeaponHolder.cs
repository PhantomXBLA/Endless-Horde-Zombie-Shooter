using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.InputSystem;

public class WeaponHolder : MonoBehaviour
{
    // Start is called before the first frame update

    [Header("WeaponToSpawn"), SerializeField]
    GameObject WeaponToSpawn;

    public PlayerController playerController;
    Sprite crosshairImage;

    [SerializeField]
    GameObject WeaponSocket;

    [SerializeField]
    GameObject IMHandSocketL;

    [SerializeField]
    GameObject IMHandSocketR;

    [SerializeField]
    Transform GripSocket;

    Animator animator;

    WeaponComponent equippedWeapon;

    public readonly int isFiringHash = Animator.StringToHash("IsFiring");
    public readonly int isReloadingHash = Animator.StringToHash("IsReloading");

    bool wasFiring = false;
    bool firingPressed = false;
    void Start()
    {
        playerController = GetComponent<PlayerController>();
        GameObject spawnedWeapon = Instantiate(WeaponToSpawn, WeaponSocket.transform.position, WeaponSocket.transform.rotation, WeaponSocket.transform);
        animator = GetComponent<Animator>();
        equippedWeapon = spawnedWeapon.GetComponent<WeaponComponent>();
        equippedWeapon.Initialize(this);
        PlayerEvents.InvokeOnWeaponEquipped(equippedWeapon);
        GripSocket = equippedWeapon.gripLocation;
    }

    // Update is called once per frame
    void Update()
    {
        
    }

    private void OnAnimatorIK(int layerIndex)
    {
        animator.SetIKPositionWeight(AvatarIKGoal.LeftHand, 1);
        animator.SetIKPosition(AvatarIKGoal.LeftHand, IMHandSocketL.transform.position);
        animator.SetIKRotationWeight(AvatarIKGoal.LeftHand, 1);
        animator.SetIKRotation(AvatarIKGoal.LeftHand, IMHandSocketL.transform.rotation);

        animator.SetIKPositionWeight(AvatarIKGoal.RightHand, 1);
        animator.SetIKPosition(AvatarIKGoal.RightHand, new Vector3(IMHandSocketR.transform.position.x, IMHandSocketR.transform.position.y, IMHandSocketR.transform.position.z));
        animator.SetIKRotationWeight(AvatarIKGoal.RightHand, 1);
        animator.SetIKRotation(AvatarIKGoal.RightHand, IMHandSocketR.transform.rotation);

        
    }

    public void OnFire(InputValue value)
    {


       

        firingPressed = value.isPressed;
        if (firingPressed)
        {
            StartFiring();
        }
        else
        {
            //print("StoppedFiring");
            StopFiring();
        }
    }

    public void StartFiring()
    {
        if (equippedWeapon.weaponStats.bulletsInMag <= 0)
        {
            StartReloading();
            return;
        }

        animator.SetBool(isFiringHash, true);
        playerController.isFiring = true;
        equippedWeapon.StartFiring();
    }

    public void StopFiring()
    {
        playerController.isFiring = false;
        animator.SetBool(isFiringHash, false);
        equippedWeapon.StopFiring();

        equippedWeapon.lineRendererL.enabled = false;
        equippedWeapon.lineRendererR.enabled = false;



    }

    public void OnReload(InputValue value)
    {
        playerController.isReloading = value.isPressed;
        
        StartReloading();
    }

    public void StartReloading()
    {
        if (equippedWeapon.isReloading || equippedWeapon.weaponStats.bulletsInMag == equippedWeapon.weaponStats.magSize) return;

        if (playerController.isFiring)
        {
            StopFiring();
        }

        if (equippedWeapon.weaponStats.reserveAmmo <= 0) return;

        animator.SetBool(isReloadingHash, true);
        equippedWeapon.StartReloading();

        InvokeRepeating(nameof(StopReloading), 0, 0.1f);



    }

    public void StopReloading()
    {
        if (animator.GetBool(isReloadingHash)) return;

        playerController.isReloading = false;
        equippedWeapon.StopReloading();
        animator.SetBool(isReloadingHash, false);
        CancelInvoke(nameof(StopReloading));
    }


}
