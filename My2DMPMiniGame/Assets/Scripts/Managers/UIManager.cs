using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;
using UnityEngine.UI;

public class UIManager : MonoBehaviour
{
    public static UIManager Instance;
    
    [SerializeField] private Text logoStartText;

    [SerializeField] private GameObject winPanel;
    [SerializeField] private Text firstPlayerWin, secondPlayerWin;


    private void Awake()
    {
        if (Instance==null)
        {
            Instance = this;
        }
    }

    private void Start()
    {
        winPanel.SetActive(false);

        firstPlayerWin.enabled = false;
        secondPlayerWin.enabled = false;
    }

    public void StartGame()
    {
        logoStartText.enabled = false;
    }

    public void WinGame(int winner)
    {
        winPanel.SetActive(true);
        if (winner==1)
        {
            firstPlayerWin.enabled = true;
            print("win1");

        }
        else if (winner==2)
        {
            secondPlayerWin.enabled = true;
            print("win2");
        }
        
    }
    
    public void PlayArena1()
    {
        SceneManager.LoadScene(TagManager.ARENA1_LEVEL_NAME);
    }
    
    public void PlayArena2()
    {
        SceneManager.LoadScene(TagManager.ARENA2_LEVEL_NAME);
    }
    
    public void PlayArena3()
    {
        SceneManager.LoadScene(TagManager.ARENA3_LEVEL_NAME);
    }
    
    public void PlayArena4()
    {
        SceneManager.LoadScene(TagManager.ARENA4_LEVEL_NAME);
    }

    public void MainMenu()
    {
        SceneManager.LoadScene(TagManager.MAIN_MENU_SCENE_NAME);
    }
}
