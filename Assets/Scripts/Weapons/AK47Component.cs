using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class AK47Component : WeaponComponent
{
    Vector3 hitLocation;

    GameObject handR;
    GameObject handL;

 

    // Start is called before the first frame update
    void Start()
    {
        lineRendererR = GameObject.Find("LineRendererR").GetComponent<LineRenderer>();
        lineRendererR.enabled = false;

        lineRendererL = GameObject.Find("LineRendererL").GetComponent<LineRenderer>();
        lineRendererL.enabled = false;

        handR = GameObject.Find("mixamorig:RightHand");
        handL = GameObject.Find("mixamorig:LeftHand");
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
                FireSound();

            }

            Ray screenRay = mainCamera.ScreenPointToRay(new Vector3(Screen.width/2, Screen.height/2, 0));

            //lineRendererR.SetPosition(1, GameObject.Find("mixamorig:RightHand").transform.position);

            if (Physics.Raycast(screenRay, out RaycastHit hit, weaponStats.fireDistance, weaponStats.weaponHitLayers))
            {
                lineRendererR.enabled = true;
                lineRendererL.enabled = true;
                

                hitLocation = hit.point;

                DealDamage(hit);

    
    

                Vector3 hitDirection = hit.point - mainCamera.transform.position;

                

                Debug.DrawRay(mainCamera.transform.position, hitDirection.normalized * weaponStats.fireDistance, Color.red, 1);

                lineRendererR.SetPosition(1, new Vector3(handR.transform.position.x, handR.transform.position.y, handR.transform.position.z));
                lineRendererR.SetPosition(0, hitLocation);

                lineRendererL.SetPosition(1, new Vector3(handL.transform.position.x, handL.transform.position.y, handL.transform.position.z));
                lineRendererL.SetPosition(0, hitLocation);

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

    void FireSound()
    {
        if(isFiring && fireSoundCanPlay)
        {
            firingSound.Play();
            fireSoundCanPlay = false;
        }
    }
}
