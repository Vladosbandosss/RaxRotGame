using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class GameManager : MonoBehaviour
{
    public static GameManager Instance;

    private void Awake()
    {
        if (Instance==null)
        {
            Instance = this;
        }
    }

    public void GameOver()
    {
        UIManager.Instance.GameOver();
    }

    public void RestartGame()
    {
        Invoke("ReloadLevel",2f);
    }

    private void ReloadLevel()
    {
        SceneManager.LoadScene(TagManager.GAME_PLAY_SCENE_NAME);
    }
}
