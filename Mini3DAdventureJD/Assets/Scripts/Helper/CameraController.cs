using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CameraController : MonoBehaviour
{
   private Transform _target;
   private Vector3 _offSet;

   [SerializeField] private float moveSpeed = 15f;
   
   private void Awake()
   {
      _target = FindObjectOfType<PlayerController>().transform;
      _offSet = transform.position;
   }

   private void Start()
   {
      Cursor.lockState = CursorLockMode.Locked;
   }

   private void Update()
   {
      FollowPlayer();
   }

   private void FollowPlayer()
   {
      transform.position = Vector3.Lerp(transform.position, _target.position + _offSet, moveSpeed * Time.deltaTime);

      if (transform.position.y<_offSet.y)
      {
         transform.position = new Vector3(transform.position.x, _offSet.y, transform.position.z);
      }
   }
}
