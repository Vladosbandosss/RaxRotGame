using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class AudioManager : MonoBehaviour
{
   public static AudioManager Instance;

   private void Awake()
   {
      if (Instance==null)
      {
         Instance = this;
      }
   }

   public AudioSource[] music;
   public AudioSource[] sfx;

   public void PlayMusic(int trackNum)
   {
      if (trackNum<music.Length)
      {
         music[trackNum].Play();
      }
   }
}
