using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class AudioManager : MonoBehaviour
{
    public static AudioManager Instance;
    
    [SerializeField] private AudioSource shootFX, exploFx;

    private void Awake()
    {
        if (Instance==null)
        {
            Instance = this;
        }
    }

    public void PlayShootFX()
    {
        shootFX.Play();
    }

    public void PlayExploFX()
    {
        exploFx.Play();
    }
}
