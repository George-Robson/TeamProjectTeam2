using System;
using System.Collections;
using System.Collections.Generic;
using UnityEditor;
using UnityEngine;
using UnityEngine.SceneManagement;

public class PauseButtons : MonoBehaviour
{
    private bool muted;
    public GameObject pauseMenu;
    public bool paused = false;

    private void Update()
    {
        if (!paused && Input.GetKeyDown(KeyCode.P))
        {
            paused = true;
            Time.timeScale = 0;
            Cursor.visible = true;
            Cursor.lockState = CursorLockMode.None;
        }
        else if (paused && Input.GetKeyDown(KeyCode.P))
        {
            paused = false;
            Time.timeScale = 1;
            Cursor.visible = false;
            Cursor.lockState = CursorLockMode.Locked;
        }
    }

    public void QuitGame()
    {
        Application.Quit();
    }

    public void MuteGame()
    {
        if (muted) {
            AudioListener.volume = 1;
            muted = false;
        }
        if (!muted)
        {
            AudioListener.volume = 0;
            muted = true;
        }
    }

    public void MainMenu()
    {
        SceneManager.LoadScene("MainMenu");
    }
}
