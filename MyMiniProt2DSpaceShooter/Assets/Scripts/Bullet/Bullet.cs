using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using Random = UnityEngine.Random;

public class Bullet : MonoBehaviour
{
   [SerializeField] private float enemyBulletMoveSpeed = 2f, playerBulletMoveSpeed = 3f;
   
   [SerializeField] private bool isPlayerBullet, isEnemyBullet;

   [SerializeField] private int enemyDamage;
   private int  _minDamage = 5, _maxDamage = 20;

   private void Start()
   {
      if (isEnemyBullet)
      {
         enemyDamage = Random.Range(_minDamage, _maxDamage);
      }
   }

   private void Update()
   {
      MoveBullet();
   }

   private void MoveBullet()
   {
      if (isPlayerBullet)
      {
         transform.position += new Vector3(0f, playerBulletMoveSpeed*Time.deltaTime, 0f);
      }

      if (isEnemyBullet)
      {
         transform.position += new Vector3(0f, -enemyBulletMoveSpeed*Time.deltaTime, 0f);
      }
   }

   private void OnTriggerEnter2D(Collider2D col)
   {
      if (col.CompareTag(TagManager.PLAYER_TAG)&&isEnemyBullet)
      {
        PlayerHealth.Instance.TakeDamage(enemyDamage);
        Destroy(gameObject);
      }

      if (col.CompareTag(TagManager.ENEMY_TAG)&&isPlayerBullet)
      {
         col.GetComponent<Enemy>().DestroyEnemyShip();
         Destroy(gameObject);
      }

      if (col.CompareTag(TagManager.ASTEROID_TAG)&&isPlayerBullet)
      {
         col.GetComponent<Asteroid>().DestroyAstroAnim();
         Destroy(gameObject);
      }
   }
}
