using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerController : MonoBehaviour
{
   private Rigidbody2D _rb;

   [SerializeField] private float moveSpeed = 4f;

   [SerializeField] private float normalPush = 10f, extraPush = 14f;
   [SerializeField] private float firstPush = 18f;

   private float _xAxis;

   private bool _isInitialPush;
   private int _pushCount;

   private bool _isPlayerDied;

   [SerializeField] private int scoreForBanana = 1, scoreForBananas = 3;
   
   private void Awake()
   {
      _rb = GetComponent<Rigidbody2D>();
   }

   private void Start()
   {
      _rb.isKinematic = true;
   }

   private void FixedUpdate()
   {
      if (Input.anyKey)
      {
         _rb.isKinematic = false;
         
         UIManager.Instance.StartGame();
      }
      
      Move();
   }

   private void Move()
   {
      if (_isPlayerDied)
      {
         return;
      }
      
      _xAxis = Input.GetAxisRaw(TagManager.HORIZONTAL_AXIS);
      _rb.velocity = new Vector2(moveSpeed * _xAxis, _rb.velocity.y);
   }

   private void OnTriggerEnter2D(Collider2D col)
   {
      if (_isPlayerDied)
      {
         return;
      }
      
      if (col.CompareTag(TagManager.EXTRA_PUSH_TAG))
      {
         if (!_isInitialPush)
         {
            _isInitialPush = true;
            
            _rb.velocity = new Vector2(_rb.velocity.x, firstPush);
            col.gameObject.SetActive(false);
            SoundManager.Instance.JumpFX();
            
            return;
         }
      }

      if (col.CompareTag(TagManager.PUSH_TAG))
      {
         _rb.velocity = new Vector2(_rb.velocity.x, normalPush);
         col.gameObject.SetActive(false);
         _pushCount++;
         SoundManager.Instance.JumpFX();
         UIManager.Instance.IncreaseScore(scoreForBanana);
      }
      
      if (col.CompareTag(TagManager.EXTRA_PUSH_TAG))
      {
         _rb.velocity = new Vector2(_rb.velocity.x, extraPush);
         col.gameObject.SetActive(false);
         _pushCount++;
         SoundManager.Instance.JumpFX();
         UIManager.Instance.IncreaseScore(scoreForBananas);
      }

      if (_pushCount==2)
      {
         _pushCount = 0;
         
         PlatformSpawner.Instance.SpawnPlatforms();
      }

      if (col.CompareTag(TagManager.FALL_DOWN_TAG) || col.CompareTag(TagManager.BIRD_TAG))
      {
         _isPlayerDied = true;
         SoundManager.Instance.GameOverFX();
         GameManager.Instance.GameOver();
      }
   }
}
