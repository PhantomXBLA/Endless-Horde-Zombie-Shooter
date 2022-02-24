using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using TMPro;

public class WeaponAmmoUI : MonoBehaviour
{

    [SerializeField] TextMeshProUGUI weaponNameText;
    [SerializeField] TextMeshProUGUI currentAmmoText;
    [SerializeField] TextMeshProUGUI reserveAmmoText;
    [SerializeField] TextMeshProUGUI FlightTimeText;


    [SerializeField] WeaponComponent weaponComponent;
    [SerializeField] MovementComponent movementComponent;



    private void OnEnable()
    {
        PlayerEvents.OnWeaponEquipped += OnWeaponEquipped;
    }

    private void OnDisable()
    {
        PlayerEvents.OnWeaponEquipped -= OnWeaponEquipped;

    }

    void OnWeaponEquipped(WeaponComponent wc)
    {
        weaponComponent = wc;
    }

    // Update is called once per frame
    void Update()
    {
        if (!weaponComponent)
        {
            return;
        }

        weaponNameText.text = weaponComponent.weaponStats.weaponName;
        currentAmmoText.text = weaponComponent.weaponStats.bulletsInMag.ToString();
        reserveAmmoText.text = weaponComponent.weaponStats.reserveAmmo.ToString();
        FlightTimeText.text = "Flight Time: " + movementComponent.flightTime.ToString();
    }
}
