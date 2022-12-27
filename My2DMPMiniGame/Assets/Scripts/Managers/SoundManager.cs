using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class SoundManager : MonoBehaviour
{
    public static SoundManager Instance;

    [SerializeField] private AudioSource[] soundFX;

    private void Awake()
    {
        
     MakeSingleton();   
     
    }

    private void MakeSingleton()
    {
        if (Instance==null)
        {
            Instance = this;
            DontDestroyOnLoad(gameObject);
        }
        else
        {
            Destroy(gameObject);
        }
    }

    public void PlaySFX(int soundIndex)
    {
        soundFX[soundIndex].Stop();
        soundFX[soundIndex].Play();
    }
}
