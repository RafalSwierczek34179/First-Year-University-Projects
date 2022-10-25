using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.InputSystem;
using UnityEngine.SceneManagement;

public class OptionsScript : MonoBehaviour
{
    public GameObject OptionsPanel;
    public GameObject GamepadObj;
    public GameObject Cursor;

    public GameObject MuteMusicButton;
    public GameObject UnMuteMusicButton;
    public GameObject MuteSFXButton;
    public GameObject UnmuteSFXButton;

    public GameObject ControllerButtonOn;
    public GameObject ControllerButtonOff;

    public static Mouse realMouse;
    public static Gamepad controller;

    public static bool muteMusic = false;
    public static bool muteSFX = false;
    [SerializeField]
    public static bool controllerToggle = false;

    private void Start()
    {
        realMouse = Mouse.current;
        controller = Gamepad.current;

        if (controller != null)
        {
            ControllerToggleON();
        }
    }

    public void changeLevel()
    {
        Time.timeScale = 1f;
        //InputSystem.AddDevice(realMouse);
        SceneManager.LoadScene("Level");
    }

    public void backToMenu()
    {
        Time.timeScale = 1f;
        InputSystem.AddDevice(realMouse);
        SceneManager.LoadScene("StartMenu");
    }

    private void DontDestroyOnLoad(Mouse realMouse)
    {
        throw new NotImplementedException();
    }

    public void OpenOptions()
    {
        OptionsPanel.SetActive(true);
    }

    public void CloseOptions()
    {
        OptionsPanel.SetActive(false);
    }

    public void MuteMusic()
    {
        muteMusic = true;
        MuteMusicButton.SetActive(false);
        UnMuteMusicButton.SetActive(true);
    }

    public void UnmuteMusic()
    {
        muteMusic = false;
        MuteMusicButton.SetActive(true);
        UnMuteMusicButton.SetActive(false);
    }

    public void MuteSFX()
    {
        muteSFX = true;
        MuteSFXButton.SetActive(false);
        UnmuteSFXButton.SetActive(true);
    }

    public void UnmuteSFX()
    {
        muteSFX = false;
        MuteSFXButton.SetActive(true);
        UnmuteSFXButton.SetActive(false);
    }

    public void ControllerToggleON()
    {
            InputSystem.DisableDevice(realMouse);

            Cursor.SetActive(true);
            GamepadObj.SetActive(true);
            GamepadObj.GetComponent<CursorMovement>().gamepadMode = true;
            Debug.Log("Gamepad true");

            controllerToggle = true;
        ControllerButtonOn.SetActive(false);
        ControllerButtonOff.SetActive(true);
    }

    public void ControllerToggleOFF()
    {
            InputSystem.EnableDevice(realMouse);

            GamepadObj.GetComponent<CursorMovement>().gamepadMode = false;
            GamepadObj.SetActive(false);
            Cursor.SetActive(false);
            Debug.Log("Gamepad false");

            controllerToggle = false;
        ControllerButtonOn.SetActive(true);
        ControllerButtonOff.SetActive(false);
    }

    public void Fullscreen()
    {
        Screen.fullScreen = !Screen.fullScreen;
        Debug.Log("Fullscreen...");
    }
}
