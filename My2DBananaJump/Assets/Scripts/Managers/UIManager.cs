using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;
using UnityEngine.UI;

public class UIManager : MonoBehaviour
{
    public static UIManager Instance;

    [SerializeField] private Text scoreText;
    private int _score;

    [SerializeField] private Text logoPrewText;

    [SerializeField] private GameObject gameOverPanel;

    private void Awake()
    {
        if (Instance==null)
        {
            Instance = this;
        }
    }

    private void Start()
    {
        scoreText.text = "Score: " +_score;
        logoPrewText.enabled = true;
        
        gameOverPanel.SetActive(false);
    }

    public void IncreaseScore(int increaseScore)
    {
        _score+=increaseScore;
        scoreText.text = "Score: " +_score;
    }

    public void StartGame()
    {
        logoPrewText.enabled = false;
    }

    public void GameOver()
    {
        gameOverPanel.SetActive(true);
    }

    public void RestartGame()
    {
        SceneManager.LoadScene(TagManager.GAME_PLAY_SCENE_NAME);
    }

    public void ExitGame()
    {
        Application.Quit();
    }
}
