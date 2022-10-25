using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.InputSystem;

public class PauseGameScript : MonoBehaviour
{
    [SerializeField] GameObject pauseScreen;
    [SerializeField] GameObject optionsPanel;
    [SerializeField] GameObject howToPanel;
    public InputAction startButton;
    public bool CanPause;

    void Start()
    {
        pauseScreen.SetActive(false);
        howToPanel.SetActive(false);
        CanPause = true;
    }

    public void OnEnable()
    {
        startButton.Enable();
    }

    public void OnDisable()
    {
        startButton.Disable();
    }

    public void Update()
    {
        if (startButton.triggered && (pauseScreen.activeSelf == false) && CanPause == true)
        {
            Debug.Log("Paused");
            Pause();
        }

        else if (startButton.triggered && (pauseScreen.activeSelf == true))
        {
            Debug.Log("Unpaused");
            Unpause();
        }
        
    }
    public void Pause()
    {
        pauseScreen.SetActive(true);
        Debug.Log("Screen up");
        Time.timeScale = 0f;
    }

    public void Unpause()
    {
        Time.timeScale = 1f;
        pauseScreen.SetActive(false);
        howToPanel.SetActive(false);
        Debug.Log("Screen down");

    }
}
