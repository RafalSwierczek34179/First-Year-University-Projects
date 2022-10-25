using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class GameOverScript : MonoBehaviour
{
    public void GameOver()
    {
        gameObject.SetActive(true);
        FindObjectOfType<PauseGameScript>().CanPause = false;
    }
}
