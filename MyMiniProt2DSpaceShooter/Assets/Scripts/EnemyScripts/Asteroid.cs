using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using Random = UnityEngine.Random;

public class Asteroid : MonoBehaviour
{
   private Animator _anim;

   [SerializeField] private int damage;
   private int _minDamage = 10, _maxDamage = 25;

   private bool _canDealDamage = true;

   [SerializeField] private float moveSpeed=1.5f;
   
   private void Awake()
   {
      _anim = GetComponent<Animator>();

      damage = Random.Range(_minDamage, _maxDamage);
   }

   private void Update()
   {
      MoveAstero();
   }

   private void MoveAstero()
   {
      transform.position += new Vector3(0f, -moveSpeed * Time.deltaTime, 0f);
   }

   public void DestroyAstroAnim()
   {
      _canDealDamage = !_canDealDamage;
      
      AudioManager.Instance.PlayExploFX();
      
      _anim.Play(TagManager.ASTRO_DESTROY_ANIMATION);
   }

   private void DestroyAstro()
   {
      Destroy(gameObject);
   }

   private void OnTriggerEnter2D(Collider2D col)
   {
      if (col.CompareTag(TagManager.PLAYER_TAG)&&_canDealDamage)
      {
         _canDealDamage = !_canDealDamage;
         
        DestroyAstroAnim();
        
        PlayerHealth.Instance.TakeDamage(damage);
      }
   }
}
