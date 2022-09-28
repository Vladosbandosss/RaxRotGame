using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class AudioManager : MonoBehaviour
{
    public static AudioManager instance;

    [SerializeField] private AudioSource[] soundFX;

    private void Awake()
    {
        if (instance==null)
        {
            instance = this;
        }
    }

    public void PlaySFX(int soundToPlay)
    {
        soundFX[soundToPlay].Stop();
        soundFX[soundToPlay].Play();
    }
}
