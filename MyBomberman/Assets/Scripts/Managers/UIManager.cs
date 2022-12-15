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
    [SerializeField] private Text liveText;
    [SerializeField] private Text bombText;
    [SerializeField] private Text rangeText;
    [SerializeField] private Text needKills;
    [HideInInspector]public int countEnemy=0;
    [SerializeField] private Text speedText;

    [HideInInspector]public bool isPaused = false;
    [SerializeField] private GameObject pausePanel;

    [SerializeField] private GameObject goPanel;

    [SerializeField] private GameObject winPanel;

    private void Awake()
    {
        if (Instance==null)
        {
            Instance = this;
        }
    }

    private void Start()
    {
        scoreText.text = "Score:0";
        pausePanel.SetActive(isPaused);
        goPanel.SetActive(false);

        GameObject[] allEnemies = GameObject.FindGameObjectsWithTag(TagManager.ENEMY_TAG);
        countEnemy = allEnemies.Length;
        
        needKills.text = "N Kills:"+countEnemy;
    }

    private void Update()
    {
        if (Input.GetKeyDown(KeyCode.Escape))
        {
            PauseGame();
        }
    }

    public void UpdateScore(int score)
    {
        scoreText.text = "Score:" + score;
    }

    public void UpdateLives(int lives)
    {
        liveText.text = "Lives:" + lives;
    }

    public void UpdateBombs(int bombs)
    {
        bombText.text = "Bombs:" + bombs;
    }

    public void UpdateRange(float range)
    {
        rangeText.text = "Range:" + range;
    }

    public void PauseGame()
    {
        if (!isPaused)
        {
            isPaused = true;
            pausePanel.SetActive(isPaused);
            Time.timeScale = 0f;
        }
        else
        {
            isPaused = false;
            pausePanel.SetActive(isPaused);
            Time.timeScale = 1f;
        }
    }

    public void ShowGameOverPanel()
    {
        goPanel.SetActive(true);
        Time.timeScale = 0f;
    }

    public void GoToMainMenu()
    {
        Time.timeScale = 1f;
        SceneManager.LoadScene("MainMenu");
    }

    public void UpdateNeedKillsTxt()
    {
        needKills.text = "N Kills:"+countEnemy;
    }

    public void ShowWinPanel()
    {
        winPanel.SetActive(true);
        Time.timeScale = 0f;
    }

    public void UpdateSpeed(float speed)
    {
        speedText.text = "Speed:" + speed;
    }
}
