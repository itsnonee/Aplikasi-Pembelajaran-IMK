using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class SceneController : MonoBehaviour
{
    public AudioController audioController;
    [Header("List Panel")]
    public GameObject MainMenu;
    public GameObject Menu;
    public GameObject AboutUs;

    public void Start()
    {
        AudioController audioController = GameObject.Find("Audio").GetComponent<AudioController>();
        MainMenu.SetActive(true);
        Menu.SetActive(false);
        AboutUs.SetActive(false);
    }

    public void GameplayScene()
    {
        audioController.ButtonSFX();
        SceneManager.LoadScene("Gameplay");
    }


    public void MainMenuPanel()
    {
        audioController.ButtonSFX();
        MainMenu.SetActive(true);
        Menu.SetActive(false);
        AboutUs.SetActive(false);
    }

    public void MenuPanel()
    {
        audioController.ButtonSFX();
        MainMenu.SetActive(false);
        Menu.SetActive(true);
        AboutUs.SetActive(false);
    }

    public void AboutUsPanel()
    {
        audioController.ButtonSFX();
        MainMenu.SetActive(false);
        Menu.SetActive(false);
        AboutUs.SetActive(true);
    }

    public void ExitGame()
    {
        audioController.ButtonSFX();
        Debug.Log("Keluar Aplikasi");
        Application.Quit();
        
    }

}
