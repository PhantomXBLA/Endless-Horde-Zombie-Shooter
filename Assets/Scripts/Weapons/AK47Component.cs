using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class AK47Component : WeaponComponent
{
    Vector3 hitLocation;
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

                DealDamage(hit);

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

    void DealDamage(RaycastHit hitInfo)
    {
        IDamagable damagable = hitInfo.collider.GetComponent<IDamagable>();
        damagable?.TakeDamage(weaponStats.damage);
    }

    private void OnDrawGizmos()
    {
        Gizmos.DrawWireSphere(hitLocation, 0.2f);
    }
}
