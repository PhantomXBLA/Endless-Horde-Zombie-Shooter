using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using TMPro;

public class HealthInfo : MonoBehaviour
{

    [SerializeField] TextMeshProUGUI currentHealthText;
    [SerializeField] TextMeshProUGUI maxHealthText;
    HealthComponent playerHealthComponent;


    // Start is called before the first frame update
    private void OnEnable()
    {
        PlayerEvents.OnHealthInitialized += OnHealthInitialized;
    }


    // Update is called once per frame
    private void OnDisable()
    {
        PlayerEvents.OnHealthInitialized -= OnHealthInitialized;
    }

    private void OnHealthInitialized(HealthComponent healthComponent)
    {
        playerHealthComponent = healthComponent;
    }

    private void Update()
    {
        currentHealthText.text = playerHealthComponent.CurrentHealth.ToString();
        maxHealthText.text = playerHealthComponent.MaxHealth.ToString();
    }
}
