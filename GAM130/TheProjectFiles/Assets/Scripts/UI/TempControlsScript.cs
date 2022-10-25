using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class TempControlsScript : MonoBehaviour
{
    public GameObject tempControls;
    private bool onoff = false;

    public void ToggleControls()
    {
        if (onoff == true)
        {
            onoff = false;
            tempControls.SetActive(false);
        }
        else if (onoff == false)
        {
            onoff = true;
            tempControls.SetActive(true);
        }
    }

}
