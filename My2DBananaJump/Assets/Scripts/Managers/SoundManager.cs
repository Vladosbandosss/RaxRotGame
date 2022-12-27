using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class SoundManager : MonoBehaviour
{
    public static SoundManager Instance;

    [SerializeField] private AudioSource soundFX;

    [SerializeField] private AudioClip jumpClip, gameOverClip;

    private void Awake()
    {
        if (Instance==null)
        {
            Instance = this;
        }
    }

    public void JumpFX()
    {
        soundFX.clip = jumpClip;
        soundFX.Play();
    }

    public void GameOverFX()
    {
        soundFX.clip = gameOverClip;
        soundFX.Play();
    }
    
    
}
