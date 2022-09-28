using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class LevelManager : MonoBehaviour
{
    public static LevelManager instance;

    [SerializeField] private float waitToRespawn=2f;

    [HideInInspector] public int gemsCollected;

    [SerializeField] private string levelToLoad;

    private void Awake()
    {
        if (instance==null)
        {
            instance = this;
        }
        
    }

    public void RespawnPlayer()
    {
        StartCoroutine("RespawnCO");
    }

    private IEnumerator RespawnCO()
    {
        UIController.instance.FadeToBlack();
        
        PlayerController.instnace.gameObject.SetActive(false);

        yield return new WaitForSeconds(waitToRespawn);
        
        UIController.instance.FadeFromBlack();
        
        PlayerController.instnace.gameObject.SetActive(true);

        PlayerController.instnace.transform.position = CheckPointController.instance.spawnPoint;

        PlayerHealth.instance.currentHealth = PlayerHealth.instance.maxHealth;
        
        UIController.instance.SetInitialHealth();
    }

    public void EndLevel()
    {
        StartCoroutine("EndLevelCO");
    }

    private IEnumerator EndLevelCO()
    {
        PlayerController.instnace.stopInput = true;
        
        UIController.instance.ShowLevelCompleteTxt();

        yield return new WaitForSeconds(1.5f);
        
        UIController.instance.FadeToBlack();
        
        yield return new WaitForSeconds(1f);

        SceneManager.LoadScene(levelToLoad);

    }
}
