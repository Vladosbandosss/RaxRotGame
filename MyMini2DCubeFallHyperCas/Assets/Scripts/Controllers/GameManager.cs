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
        StartCoroutine(GameOverCO());
    }

    private IEnumerator GameOverCO()
    {
        AudioController.Instance.PlayGameOverSfx();
        
        PlayerController.Instance.PlayDeadFX();
        
        UIController.Instance.makeDarkFade = true;
        
        yield return new WaitForSeconds(2f);
        
        UIController.Instance.makeDarkFade = false;
        
        UIController.Instance.makeNormalFade = true;

        SceneManager.LoadScene("GamePlay");
    }
}
