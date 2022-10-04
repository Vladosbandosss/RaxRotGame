using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerController : MonoBehaviour
{
   public static PlayerController Instance;
   
   private Rigidbody2D _rb;

   [SerializeField] private float moveSpeed = 2.5f;
   private float _hAxis;

   [SerializeField] private float minXPosition = -2.5f, maxXPosition = 2.5f;
   [SerializeField] private float minYPosition = -7f;
   private Vector2 _temp;

   [SerializeField] private GameObject deadFX;

   private void Awake()
   {
      if (Instance==null)
      {
         Instance = this;
      }
      
      _rb = GetComponent<Rigidbody2D>();
   }

   private void Update()
   {
      MovePlayer();
   }

   private void MovePlayer()
   {
      _hAxis = Input.GetAxisRaw(TagManager.HORIZONTAL_AXIS) * moveSpeed;

      if (_hAxis!=0)
      {
         _rb.velocity = new Vector2(_hAxis, _rb.velocity.y);
      }
      
      CheckBorder();
   }
   
   public void MovePlayerInMovingPlatform(float moveForce)
   {
      _rb.velocity = new Vector2(moveForce, _rb.velocity.y);
   }

   private void CheckBorder()
   {
      _temp = transform.position;
      
      if (_temp.x>maxXPosition)
      {
         _temp.x = maxXPosition;
      }

      if (_temp.x<minXPosition)
      {
         _temp.x = minXPosition;
      }

      if (_temp.y<minYPosition)
      {
        GameManager.Instance.GameOver();
      }

      transform.position = _temp;
   }

   public void PlayDeadFX()
   {
      Instantiate(deadFX, transform.position, Quaternion.identity);
      gameObject.SetActive(false);
   }
   
}
