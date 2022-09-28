using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class PauseMenuController : MonoBehaviour
{
    [SerializeField] private GameObject pauseScreen;
    [SerializeField]private bool isPaused;

    
    public void PauseUnPause(bool isPaused)
    {
        if (isPaused)
        {
            Time.timeScale = 0f;
            pauseScreen.SetActive(true);
        }
        else
        {
            Time.timeScale = 1f;
            pauseScreen.SetActive(false);
        }
    }

    public void LevelSelect()
    {
        Time.timeScale = 1f;
    }

    public void MainMenu()
    {
        Time.timeScale = 1f;
        SceneManager.LoadScene("MainMenu");
    }
}
