using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerHealth : MonoBehaviour
{
   public static PlayerHealth Instance;

   public int maxHealth, currentHealth;

   private void Awake()
   {
      if (Instance==null)
      {
         Instance = this;
      }
   }

   private void Start()
   {
      currentHealth = maxHealth;

      UIController.Instance.healthSlider.maxValue = maxHealth;
      UIController.Instance.healthSlider.value = currentHealth;
      
   }

   public void DamagePlayer(int damageAmount)
   {
      if (!GameManager.Instance.loadingNextLevel)
      {
         currentHealth -= damageAmount;
      
         UIController.Instance.ShowDamage();
      
         AudioManager.Instance.PlaySFX(11);

         if (currentHealth<=0)
         {
            currentHealth = 0;
            gameObject.SetActive(false);
         
            GameManager.Instance.PlayerDied();
         
            StopMusic();
         }

         UIController.Instance.healthSlider.value = currentHealth;
      }
   }

   public void HealPlayer(int healAmount)
   {
      currentHealth += healAmount;

      if (currentHealth>maxHealth)
      {
         currentHealth = maxHealth;
      }
      
      UIController.Instance.healthSlider.value = currentHealth;
   }

   private void StopMusic()
   {
      StopCoroutine(StopMusicCO());
   }

   private IEnumerator StopMusicCO()
   {
      AudioManager.Instance.PlaySFX(13);

      yield return new WaitForSeconds(2f);
      
      AudioManager.Instance.StopBGM();
      
   }
}
