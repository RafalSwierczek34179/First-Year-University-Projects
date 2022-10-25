using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;
using UnityEngine.UI;
using System;

public class GameOver_script : MonoBehaviour
{

    bool gameOver = false;
    string minutes = "mins ";
    string seconds = "secs";

    // Gets called when the player dies
    public void TurnOnGameOverScript()
    {
        if (gameOver == false)
        {
            gameOver = true;
            this.transform.Find("GameOverCanvas").gameObject.SetActive(true);

            this.transform.Find("GameOverCanvas/TimeText").gameObject.transform.GetComponent<Text>().text = "TIME: " 
                + TimeSpan.FromSeconds(GameObject.Find("HUD/Bar").gameObject.GetComponent<Stopwatch>().currentTime).ToString(@"mm\:ss");

            this.transform.Find("GameOverCanvas/ScoreText").gameObject.transform.GetComponent<Text>().text = "SCORE: " + GameObject.Find("HUD/Bar").gameObject.GetComponent<Stopwatch>().scoreText.text;

            Time.timeScale = 0.1f;
            Invoke("UnlockCursor", 0.305f);
        }
    }

    void UnlockCursor()
    {
        Cursor.lockState = CursorLockMode.None;
        Cursor.visible = true;

        Time.timeScale = 0f;
        
    }

    public void RetryButtonPress()
    {
        Time.timeScale = 1f;
        SceneManager.LoadScene(SceneManager.GetActiveScene().buildIndex);
    }

    public void MenuButtonPress()
    {
        Time.timeScale = 1f;
        SceneManager.LoadScene(0);
    }

    public void QuitButtonPress()
    {
        Application.Quit();
    }

}
