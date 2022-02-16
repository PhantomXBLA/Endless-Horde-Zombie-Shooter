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
        GripSocket = equippedWeapon.gripLocation;
    }

    // Update is called once per frame
    void Update()
    {
        
    }

    private void OnAnimatorIK(int layerIndex)
    {
        animator.SetIKPositionWeight(AvatarIKGoal.LeftHand, 1);
        animator.SetIKPosition(AvatarIKGoal.LeftHand, GripSocket.transform.position);
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
            print("StoppedFiring");
            StopFiring();
        }
    }

    public void StartFiring()
    {
        if (equippedWeapon.weaponStats.bulletsInMag <= 0) return;
        animator.SetBool(isFiringHash, true);
        playerController.isFiring = true;
        equippedWeapon.StartFiring();
    }

    public void StopFiring()
    {
        animator.SetBool(isFiringHash, false);
        playerController.isFiring = false;
        equippedWeapon.StopFiring();




    }

    public void OnReload(InputValue value)
    {
        playerController.isReloading = value.isPressed;
        animator.SetBool(isReloadingHash, playerController.isReloading);
    }

    public void StartReloading()
    {

    }


}
