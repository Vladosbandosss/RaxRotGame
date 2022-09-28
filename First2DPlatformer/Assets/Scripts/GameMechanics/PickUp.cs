using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PickUp : MonoBehaviour
{
   [SerializeField] private bool isGem, isHeal;

   [SerializeField] private GameObject pickUpFX;
   
   private void OnTriggerEnter2D(Collider2D col)
   {
      if (col.CompareTag(TagManager.PLAYER_TAG))
      {
         
         if (isGem)
         {
            LevelManager.instance.gemsCollected++;
            
            UIController.instance.GemCollected();

            Destroy(gameObject);
            
            Instantiate(pickUpFX, transform.position, transform.rotation);
            
            AudioManager.instance.PlaySFX(6);
         }
         
         if (isHeal)
         {
            if (PlayerHealth.instance.currentHealth!=PlayerHealth.instance.maxHealth)
            {
               PlayerHealth.instance.HealPlayer();
               Destroy(gameObject);
               
               Instantiate(pickUpFX, transform.position, transform.rotation);
               
               AudioManager.instance.PlaySFX(7);
            }
         }
      }
   }
}
