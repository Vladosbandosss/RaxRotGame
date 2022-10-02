using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class AudioManager : MonoBehaviour
{
    public static AudioManager Instance;

    [SerializeField] private AudioSource bgm;

    [SerializeField] private AudioSource[] soundFX;

    private void Awake()
    {
        if (Instance==null)
        {
            Instance = this;
        }
    }

    public void StopBGM()
    {
        bgm.Stop();
    }

    public void PlaySFX(int sfxNumber)
    {
        soundFX[sfxNumber].Stop();
        soundFX[sfxNumber].Play();
    }

    public void StopSFX(int sfxNumber)
    {
        soundFX[sfxNumber].Stop();
    }
}
