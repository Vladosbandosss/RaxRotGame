using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Bullet : MonoBehaviour
{
   [SerializeField] private GameObject deadFX;
   [SerializeField] private float moveSpeed=5f;

   private void Update()
   {
      Movement();
   }

   private void Movement()
   {
      transform.position+=new Vector3(-moveSpeed*Time.deltaTime,0f,0f);
   }

   private void OnCollisionEnter2D(Collision2D col)
   {
      if (col.gameObject.CompareTag(TagManager.PLAYER_TAG))
      {
         Instantiate(deadFX, transform.position, Quaternion.identity);
         
         PlayerHealth.instance.DealDamage();
         
      }
      
      Destroy(gameObject);
   }

   private void OnTriggerEnter2D(Collider2D col)
   {
      Instantiate(deadFX, transform.position, Quaternion.identity);
     
      Destroy(gameObject);
   }
}
