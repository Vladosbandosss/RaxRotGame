using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CameraController : MonoBehaviour
{
   private Transform _target;

   [SerializeField] private Transform farBg, middleBg;

   [SerializeField] private float minHeight=-1.5f, maxHeight=2.5f;

   private Vector2 _lastPos;

   private void Awake()
   {
      _target = GameObject.FindWithTag(TagManager.PLAYER_TAG).transform;
   }

   private void Start()
   {
      _lastPos = transform.position;
   }

   private void LateUpdate()
   {

      if (!_target)
      {
         return;
      }

      FollowTarget();

   }

   private void FollowTarget()
   {
      //float clapedY = Mathf.Clamp(_target.position.y+2f, minHeight, maxHeight);
      
      transform.position = new Vector3(_target.position.x, transform.position.y, transform.position.z);

      //Vector2 amountToMove =new Vector2(transform.position.x-_lastPos.x,transform.position.y-_lastPos.y);

      //farBg.position += new Vector3(amountToMove.x, amountToMove.y, 0f);
      //middleBg.position += new Vector3(amountToMove.x * 0.5f, amountToMove.y*0.5f, 0f);

      //_lastPos = transform.position;
   }
}
