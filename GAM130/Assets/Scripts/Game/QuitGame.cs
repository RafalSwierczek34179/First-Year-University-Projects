using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class QuitGame : MonoBehaviour
{
    public void quit()
    {
        Debug.Log("Quitting Game...");
        Application.Quit();
    }
}