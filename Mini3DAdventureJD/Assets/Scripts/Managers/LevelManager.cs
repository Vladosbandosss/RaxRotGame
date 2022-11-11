using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class LevelManager : MonoBehaviour
{
    public static LevelManager Instance;

    [SerializeField] private float waitBeforeRespawn = 2f, waitBeforeLoading = 2f;
    
    [HideInInspector] public bool respawning;

    private PlayerController _playerController;

    [HideInInspector] public Vector3 respawnPoint;
    
    

    private void Awake()
    {
        if (Instance==null)
        {
            Instance = this;
        }
    }

    private void Start()
    {
        _playerController = FindObjectOfType<PlayerController>();
        respawnPoint = _playerController.transform.position+Vector3.up;
        
        UIController.Instance.FadeFromBlack();
    }

    public void RespawnPlayer()
    {
        if (!respawning)
        {
            respawning = true;

            StartCoroutine(RespawnCo());
        }
    }

    private IEnumerator RespawnCo()
    {
        UIController.Instance.FadeToBlack();
        
        _playerController.gameObject.SetActive(false);
        
        yield return new WaitForSeconds(waitBeforeRespawn);
        
        UIController.Instance.FadeFromBlack();
        
        _playerController.transform.position=respawnPoint;
        _playerController.gameObject.SetActive(true);

        respawning = false;
        
        PlayerHealthController.Instance.FillHealth();
    }

    public void LoadNextLevel(string nexLevel)
    {
        StartCoroutine(LoadNextLevelCo(nexLevel));
    }

    private IEnumerator LoadNextLevelCo(string nextLevel)
    {
        UIController.Instance.FadeToBlack();
        
        _playerController.anim.SetTrigger(TagManager.PLAYER_END_LEVEL);

        yield return new WaitForSeconds(waitBeforeLoading);

        SceneManager.LoadScene(nextLevel);
    }
}
