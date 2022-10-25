using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class WaveInfoScreen : MonoBehaviour
{
    [SerializeField] GameObject WaveOverInfoScreen;
    // Start is called before the first frame update
    public void ContinueButton()
    {
        Time.timeScale = 1f;
        WaveOverInfoScreen.SetActive(false);
    }
}
