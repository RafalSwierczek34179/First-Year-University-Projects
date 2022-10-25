using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.InputSystem;

public class CursorManager : MonoBehaviour
{
    public GameObject GamepadObj;
    public GameObject Cursor;

    public static Mouse realMouse;
    public static Gamepad controller;

    public static bool controllerToggle = false;

    public void OnEnable()
    {
        realMouse = Mouse.current;
        controller = Gamepad.current;

        if (controller != null)
        {
            ControllerToggle();
            Debug.Log("Poop mutant");
        }
    }

    public void ControllerToggle()
    {
        if (controllerToggle == false)
        {
            InputSystem.DisableDevice(realMouse);

            Cursor.SetActive(true);
            GamepadObj.SetActive(true);
            GamepadObj.GetComponent<CursorMovement>().gamepadMode = true;
            Debug.Log("Gamepad true");

            controllerToggle = true;
        }
        else if (controllerToggle)
        {
            InputSystem.EnableDevice(realMouse);

            GamepadObj.GetComponent<CursorMovement>().gamepadMode = false;
            GamepadObj.SetActive(false);
            Cursor.SetActive(false);
            Debug.Log("Gamepad false");

            controllerToggle = false;
        }
    }
}
