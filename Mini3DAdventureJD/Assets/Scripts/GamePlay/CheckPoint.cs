using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CheckPoint : MonoBehaviour
{
   [SerializeField] private Animator anim;
   
   [SerializeField] private GameObject checkPointFX;
   [SerializeField] private Transform checkPointSpawnPosition;
   private bool _canCreateFX = true;
   
   private void OnTriggerEnter(Collider other)
   {
      if (other.CompareTag(TagManager.PLAYER_TAG)&&_canCreateFX)
      {
         anim.SetTrigger(TagManager.CHECKPOINT_ANIMATION_TRIGGER);
         
         _canCreateFX = false;
         
         LevelManager.Instance.respawnPoint = transform.position;
         
         Instantiate(checkPointFX, checkPointSpawnPosition.position, Quaternion.identity);
      }
   }
}
