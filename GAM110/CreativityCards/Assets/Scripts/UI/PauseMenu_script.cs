using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.InputSystem;
using UnityEngine.UI;
using UnityEngine.SceneManagement;

public class PauseMenu_script : MonoBehaviour
{
    // GameObjects
    [SerializeField]
    GameObject pauseMenuScreen;
    [SerializeField]
    GameObject player;
    // Sub Menus
    [SerializeField]
    GameObject volumeSettingsUI;
    [SerializeField]
    GameObject fovSettingsUI;
    [SerializeField]
    GameObject restartConfirmationUI;
    [SerializeField]
    GameObject quitConfirmationUI;
    
    // Temp var just representing volume level until i can actually link it to the volume
    [SerializeField]
    float volume;
    [SerializeField]
    float fov;
    // Slows the game down to X% of its intended speed when the pause menu is up
    [SerializeField]
    float pauseMenuTimeScale = 0.03f;

    private void Start()
    {
        Cursor.lockState = CursorLockMode.Locked;
        Cursor.visible = false;
    }

    void Update()
    {
        // Can't use the standard "GetKey(keycode.escape)" because the 3rd person template comes with its own input system managemnt system which overrides the default unity one
        if (Keyboard.current.escapeKey.wasReleasedThisFrame)
        {
            TogglePauseMenu();
        }

        volumeSettingsUI.transform.GetComponentInChildren<Slider>().value = volume;

        // May seem unnecessary to have fov variable here, but later on this is going to be used to permamently store the users set fov across scenes
        fovSettingsUI.transform.GetComponentInChildren<Slider>().value = fov;
        Camera.main.fieldOfView = fov;
        
    }

    // ~~~~~~~~~~~~~~~~~~~~~~~~~~~Buttons being pressed~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~

    public void ResumeButtonPress()
    {
        // Opens or closes the pause menu based on whether its already open or not, in this case it will always close
        TogglePauseMenu();
    }

    public void VolumeButtonPress()
    {
        TurnOffSubPauseMenus();
        volumeSettingsUI.SetActive(true);
    }

    public void FOVButtonPress()
    {
        TurnOffSubPauseMenus();
        fovSettingsUI.SetActive(true);
    }

    public void RestartButtonPress()
    {
        TurnOffSubPauseMenus();
        restartConfirmationUI.SetActive(true);
    }

    public void RestartConfirmationYesButtonPress()
    {
        // Time scale stays the same even after reloading the scene, which is why i make sure its set to normal game speed before resetting the scene
        Time.timeScale = 1;
        SceneManager.LoadScene(SceneManager.GetActiveScene().buildIndex);
    }

    public void RestartConfirmationNoButtonPress()
    {
        TurnOffSubPauseMenus();
    }

    public void QuitButtonPress()
    {
        TurnOffSubPauseMenus();
        quitConfirmationUI.SetActive(true);
    }

    public void QuitConfirmationYesButtonPress()
    {
        Application.Quit();
    }

    public void QuitConfirmationNoButtonPress()
    {
        TurnOffSubPauseMenus();
    }

    // ~~~~~~~~~~~~~~~~~~~~~~~~~~End of buttons being pressed~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~

    // Turns off all of the sub menus that appear in the middle of the pause menu, useful for when closing the pause menu or switching to a different sub menu
    private void TurnOffSubPauseMenus()
    {
        volumeSettingsUI.SetActive(false);
        fovSettingsUI.SetActive(false);
        restartConfirmationUI.SetActive(false);
        quitConfirmationUI.SetActive(false);
    }

    public void VolumeSliderChange()
    {
        volume = volumeSettingsUI.transform.GetComponentInChildren<Slider>().value;
    }

    public void FOVSliderChange()
    {
        fov = fovSettingsUI.transform.GetComponentInChildren<Slider>().value;
    }

    void TogglePauseMenu()
    {
        // Turning the pause menu ON
        if (pauseMenuScreen.activeSelf == false)
        {
            pauseMenuScreen.SetActive(true);

            // Stops the player from moving
            player.transform.GetComponent<PlayerInput>().DeactivateInput();

            // Slows down the game
            Time.timeScale = pauseMenuTimeScale;

            Cursor.visible = true;
            Cursor.lockState = CursorLockMode.None;
        }
        // Turning the pause menu OFF
        else
        {

            TurnOffSubPauseMenus();
            pauseMenuScreen.SetActive(false);

            // Allows the player to move again
            player.transform.GetComponent<PlayerInput>().ActivateInput();
            Time.timeScale = 1;

            Cursor.visible = false;
            Cursor.lockState = CursorLockMode.Locked;

        }
    }

}
