using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class AudioManager : MonoBehaviour
{
    public static AudioManager Instance;
    
    private AudioSource _audioManager;

    [SerializeField] private AudioClip bombExploded, bombPlaced, enemyDead, playerDead, powerPickUp, powerSpawn;

    private void Awake()
    {
        if (Instance==null)
        {
            Instance = this;
        }
        DontDestroyOnLoad(gameObject);
        
        _audioManager = GetComponent<AudioSource>();
    }

    public void BombExplodedSFX()
    {
        _audioManager.PlayOneShot(bombExploded);
    }

    public void BombPlacedSFX()
    {
        _audioManager.PlayOneShot(bombPlaced);
    }

    public void EnemyDeadSFX()
    {
        _audioManager.PlayOneShot(enemyDead);
    }

    public void PlayerDeadSFX()
    {
        _audioManager.PlayOneShot(playerDead);
    }

    public void PowerPickUpSFX()
    {
        _audioManager.PlayOneShot(powerPickUp);
    }

    public void powerSpawnSFX()
    {
        _audioManager.PlayOneShot(powerSpawn);
    }
}
