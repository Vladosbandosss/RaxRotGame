using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class GameManager : MonoBehaviour
{
    public static GameManager Instance;
    
    private int lives=3;

    [SerializeField] private GameObject playerPrefab;
    private Vector3 spawnPlayerVector = new Vector3(0f, 0f, 0f);

    private bool _isPlayerIsActive = false;
    private float _timeToRespawm = 2f;

    private int _score = 0;

    [HideInInspector] public float bombRange = 2;
    [SerializeField] private float maxBombRange = 5;

    private void Awake()
    {
        if (Instance==null)
        {
            Instance = this;
        }
    }

    private void Start()
    {
        Instantiate(playerPrefab, spawnPlayerVector, Quaternion.identity);
        PlayerController.Instance.hasControl = true;
        
        UIManager.Instance.UpdateLives(lives);
        UIManager.Instance.UpdateRange(bombRange);
    }
    
    public void PlayerDied()
    {
        lives--;

        if (lives<=0)
        {
            AudioManager.Instance.PlayerDeadSFX();
            
            lives = 0;
            UIManager.Instance.UpdateLives(lives);
            print("Hello from GM,player has 0 lives");
            
            PlayerController.Instance.hasControl = false;
            DeadPlayerAndRespawn();
        }
        else
        {
            UIManager.Instance.UpdateLives(lives);
            print("I have lives" + lives);
        }
    }

    private void DeadPlayerAndRespawn()
    {
        StartCoroutine("DeadPlayerAndRespawnCO");
    }
    
    private IEnumerator DeadPlayerAndRespawnCO()
    {
        for (int i = 0; i < 20; i++)
        {
            yield return new WaitForSeconds(0.2f);
            PlayerController.Instance.gameObject.SetActive(_isPlayerIsActive);
            _isPlayerIsActive = !_isPlayerIsActive;
        }
        
        PlayerController.Instance.gameObject.SetActive(false);
        
        yield return new WaitForSeconds(_timeToRespawm);
        
        UIManager.Instance.ShowGameOverPanel();

        //SceneManager.LoadScene(SceneManager.GetActiveScene().name);
        
        
    }

    public void UpdateScore(int score)
    {
        _score += score;
        
        UIManager.Instance.UpdateScore(_score);
    }

    public void EnemyDead()
    {
        UIManager.Instance.countEnemy--;

        if (UIManager.Instance.countEnemy<=0)
        {
            UIManager.Instance.countEnemy = 0;
            WinGame();
        }
        UIManager.Instance.UpdateNeedKillsTxt();
    }

    private void WinGame()
    {
        print("Win");

        switch (SceneManager.GetActiveScene().name)
        {
            case "Level1":
                SceneManager.LoadScene("Level2");
                break;
            case "Level2":
                UIManager.Instance.ShowWinPanel();
                break;
        }
    }

    public void IncreaseRange()
    {
        bombRange++;
        if (bombRange>maxBombRange)
        {
            bombRange = maxBombRange;
        }
        UIManager.Instance.UpdateRange(bombRange);
    }
}
