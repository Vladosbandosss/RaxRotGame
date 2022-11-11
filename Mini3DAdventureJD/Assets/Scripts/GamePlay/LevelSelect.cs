using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class LevelSelect : MonoBehaviour
{
   [SerializeField] private string levelName;

   [SerializeField] private GameObject mapPointActive, mapPointInactive;

   private void Start()
   {
      mapPointInactive.SetActive(true);
   }


   private void OnTriggerEnter(Collider other)
   {
      if (other.CompareTag(TagManager.PLAYER_TAG))
      {
         mapPointInactive.SetActive(false);
         mapPointActive.SetActive(true);
         
         StartCoroutine(LoadLevelCo());
      }
   }
   
   private IEnumerator LoadLevelCo()
   {
      FindObjectOfType<PlayerController>().stopMoving = true;
      
      UIController.Instance.FadeToBlack();
      
      yield return new WaitForSeconds(2f);

      SceneManager.LoadScene(levelName);


   }
}
