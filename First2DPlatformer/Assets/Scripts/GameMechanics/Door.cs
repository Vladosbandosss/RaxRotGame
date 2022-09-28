using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Door : MonoBehaviour
{
   public static Door instance;
   
   private Animator _anim;

   private void Awake()
   {
      if (instance==null)
      {
         instance = this;
      }
      
      _anim = GetComponent<Animator>();
   }

   public void OpenDoor()
   {
      _anim.SetTrigger(TagManager.OPEN_DOOR_TRIGGER);
   }
}
