using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class WinSceneScript : MonoBehaviour
{
    public AudioSource allzombieDie;
    // Start is called before the first frame update
    void Start()
    {
        Cursor.visible = true;
        Cursor.lockState = CursorLockMode.None;
        Invoke("PlaySound", 9);   
    }

    // Update is called once per frame
    void Update()
    {
        
    }

    void PlaySound()
    {
        allzombieDie.Play();
    }
}
