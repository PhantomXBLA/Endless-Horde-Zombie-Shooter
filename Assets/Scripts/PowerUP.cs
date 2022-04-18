using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PowerUP : MonoBehaviour
{
    public MovementComponent playerMovementComponent;
    // Start is called before the first frame update
    void Start()
    {
        playerMovementComponent = GameObject.Find("Player").GetComponent<MovementComponent>();
    }

    // Update is called once per frame
    void Update()
    {
        
    }

    private void OnCollisionEnter(Collision collision)
    {
        if(collision.gameObject.name == "Player")
        {
            playerMovementComponent.flightTime = playerMovementComponent.maxFlightTime;
            playerMovementComponent.flightRestored.Play();
            Destroy(this.gameObject);
        }
    }
}
