using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class AudioController : MonoBehaviour
{
    public static AudioController Instance;

    [SerializeField] private AudioSource landSfx,goSfx;
    private void Awake()
    {
        if (Instance==null)
        {
            Instance = this;
        }
    }

    public void PlayLandSfx()
    {
        landSfx.Play();
    }

    public void PlayGameOverSfx()
    {
        goSfx.Play();
    }
    
}
