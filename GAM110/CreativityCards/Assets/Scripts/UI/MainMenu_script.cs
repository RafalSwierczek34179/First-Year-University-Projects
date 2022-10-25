using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.InputSystem;
using UnityEngine.UI;
using UnityEngine.SceneManagement;

public class MainMenu_script : MonoBehaviour
{

    GameObject screen;
    GameObject titleScreenPanel;
    GameObject fadeOutScreen;
    GameObject gameLoadingScreen;
    GameObject mainMenuPanel;
    GameObject blackPanel;
    GameObject creditsPage;
    

    bool titleScreenOn = true;

    private void Start()
    {
        Cursor.lockState = CursorLockMode.Locked;
        Cursor.visible = false;

        // This is to stop game objects from being dragged into the script through the editor, note that if the objects name changes then it needs to be changed here manually
        screen = this.transform.Find("Screen").gameObject;
        mainMenuPanel = screen.transform.Find("MainMenuPanel").gameObject;
        fadeOutScreen = screen.transform.Find("FadeOutScreen").gameObject;
        gameLoadingScreen = screen.transform.Find("GameLoadingScreen").gameObject;
        titleScreenPanel = screen.transform.Find("TitleScreenPanel").gameObject;
        creditsPage = screen.transform.Find("CreditsPage").gameObject;
        blackPanel = screen.transform.Find("BlackPanel").gameObject;
    }

    // Update is called once per frame
    void Update()
    {
        if (Keyboard.current.anyKey.wasPressedThisFrame && titleScreenOn)
        {
            fadeOutScreen.SetActive(true);
            titleScreenOn = false;
            Invoke("TurnOffTitleScreen", 1f);
        }
    }

    // Buttons Being Pressed ~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~

    public void PlayButtonPressed()
    {
        Cursor.visible = false;
        Cursor.lockState = CursorLockMode.Locked;

        gameLoadingScreen.SetActive(true);
        Invoke("GameLoadingScreen", 0.5f);
    }

    public void HelpButtonPress()
    {
        blackPanel.SetActive(true);
        Invoke("TurningOnHelpPage", 1f);
    }

    public void BackButtonPress()
    {
        screen.transform.Find("HelpPage").gameObject.SetActive(false);
        blackPanel.SetActive(false);
    }

    public void CreditsButtonPress()
    {
        blackPanel.SetActive(true);
        creditsPage.SetActive(true);

        Cursor.lockState = CursorLockMode.Locked;
        Cursor.visible = false;

        Invoke("TurnOffCredits", 17f);
    }

    public void QuitButtonPress()
    {
        Application.Quit();
    }

    //End Of Buttons Being Pressed ~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~

    void TurnOffCredits()
    {
        blackPanel.SetActive(false);
        creditsPage.SetActive(false);

        Cursor.lockState = CursorLockMode.None;
        Cursor.visible = true;
    }

    void TurnOffTitleScreen()
    {
        titleScreenPanel.SetActive(false);
        mainMenuPanel.SetActive(true);
        Invoke("EnableMainMenuScreen", 1f);
    }

    // After the title card, a small animation plays to load in the main menu, after this there's an invisible panel on the screen so this function
    // is necessary to allow the player to interact with the menu
    void EnableMainMenuScreen()
    {
        // Disables an invisible panel
        fadeOutScreen.SetActive(false);

        Cursor.lockState = CursorLockMode.None;
        Cursor.visible = true;
    }

    void GameLoadingScreen()
    {
        mainMenuPanel.SetActive(false);
        Invoke("FadeOutIntoFirstLevel", 4f);
    }
    void FadeOutIntoFirstLevel()
    {
        blackPanel.SetActive(true);
        Invoke("LoadFirstLevel", 1f);
    }
    void LoadFirstLevel()
    {
        SceneManager.LoadScene(SceneManager.GetActiveScene().buildIndex + 1);
    }

    // In it's own function so that it can be invoked after 1 second, this allows the fading animation to complete before loading in sub menu
    void TurningOnHelpPage()
    {
        screen.transform.Find("HelpPage").gameObject.SetActive(true);
    }
}
