using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class MusicScript : MonoBehaviour
{
    public AudioSource MusicObject;
    public AudioSource MenuButton;
    public float Volume;

    private void Start()
    {
        if (OptionsScript.muteMusic == false)
        {
            MusicObject.volume = Volume;
        }
        else if (OptionsScript.muteMusic == true)
        {
            MusicObject.volume = 0f;
        }
    }
    public void Music()
    { 
    if (OptionsScript.muteMusic == false)
        {
            MusicObject.volume = Volume;
        }
    else if (OptionsScript.muteMusic == true)
        {
            MusicObject.volume = 0f;
        }
    }

    public void ButtonClick()
    {
        if (OptionsScript.muteSFX == false)
        {
         MenuButton.Play();
        }
    }
}
