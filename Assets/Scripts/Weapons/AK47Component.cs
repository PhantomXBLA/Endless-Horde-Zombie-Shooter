using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class AK47Component : WeaponComponent
{
    
    // Start is called before the first frame update
    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {
        
    }

    protected override void Fire()
    {
        Vector3 hitLocation;

        if(weaponStats.bulletsInMag > 0 && !isReloading && !weaponHolder.playerController.isRunning)
        {
            base.Fire();

            if (firingEffect)
            {
                firingEffect.Play();
            }

            Ray screenRay = mainCamera.ScreenPointToRay(new Vector3(Screen.width/2, Screen.height/2, 0));

            if (Physics.Raycast(screenRay, out RaycastHit hit, weaponStats.fireDistance, weaponStats.weaponHitLayers))
            {

                hitLocation = hit.point;

                Vector3 hitDirection = hit.point - mainCamera.transform.position;
                Debug.DrawRay(mainCamera.transform.position, hitDirection.normalized * weaponStats.fireDistance, Color.red, 1);

                print("Hit Something");
            }

            print("Bullet count: " + weaponStats.bulletsInMag);

        }
        
        else if (weaponStats.bulletsInMag <= 0)
        {
            weaponHolder.StartReloading();
        }
        
    }
}
