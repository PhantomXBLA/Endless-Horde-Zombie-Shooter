using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class UniBeamCharge : MonoBehaviour
{
    public PlayerController playerController;
    // Start is called before the first frame update
    void Start()
    {
        playerController = GameObject.Find("Player").GetComponent<PlayerController>();
    }

    // Update is called once per frame
    void Update()
    {

    }

    private void OnCollisionEnter(Collision collision)
    {
        if (collision.gameObject.name == "Player")
        {
            playerController.IncrementUnibeam(1);
            Destroy(this.gameObject);
        }
    }
}
