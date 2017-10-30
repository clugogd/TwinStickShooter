using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class HUDController : MonoBehaviour
{

    public GameObject MainMenu;
    public GameObject OptionsMenu;
    public GameObject HUD;
    public GameObject Banner;

    // Use this for initialization
    void Start()
    {

    }

    // Update is called once per frame
    void Update()
    {
        if (Input.GetButtonDown("START"))
            DisableMainMenu();
        if (Input.GetButtonDown("BACK"))
            EnableOPTIONS();
    }

    public void ExitApplication()
    {
#if UNITY_EDITOR
        UnityEditor.EditorApplication.isPlaying = false;
#else
        Application.Quit();
#endif
    }
    public void DisableHUD()
    {
        HUD.SetActive(false);
    }
    public void EnableHUD()
    {
        HUD.SetActive(true);
    }
    public void EnableOPTIONS()
    {
        OptionsMenu.SetActive(true);
    }
    public void DisableOPTIONS()
    {
        OptionsMenu.SetActive(false);
    }
    public void DisableMainMenu()
    {
        MainMenu.SetActive(false);
    }
    public void EnableMainMenu()
    {
        MainMenu.SetActive(true);
    }

    public void ShowBanner(string displayMessage)
    {
        Banner.SetActive(true);
        Banner.GetComponentInChildren<Text>().text = displayMessage;
    }
    public void HideBanner()
    {
        Banner.SetActive(false);
    }
}
