using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class StartGameScript : MonoBehaviour
{
    // Start is called before the first frame update

    public Canvas creditsCanvas;
    public Canvas menuCanvas;
    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {
        
    }

    public void StartButtonPressed()
    {
        SceneManager.LoadScene("GameScene");
    }

    public void MenuButtonPressed()
    {
        SceneManager.LoadScene("MenuScene");
    }

    public void CreditsButtonPressed()
    {
        creditsCanvas.gameObject.SetActive(true);
        menuCanvas.gameObject.SetActive(false);

        Invoke("ReturnToMenu", 2);


    }

    public void ReturnToMenu()
    {
        SceneManager.LoadScene("MenuScene");
    }
}
