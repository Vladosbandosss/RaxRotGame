using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class MovingPlatform : MonoBehaviour
{
   [SerializeField] private Transform[] pointsToMove;
   private int _currentPointToMove;

   [SerializeField] private float moveSpeed=2f;

   [SerializeField] private Transform platform;

   private void Update()
   {
      MovePlatform();
   }

   private void MovePlatform()
   {
      platform.position = Vector3.MoveTowards(platform.position, pointsToMove[_currentPointToMove].position, moveSpeed*Time.deltaTime);

      if (Vector3.Distance(platform.position,pointsToMove[_currentPointToMove].position)<0.2f)
      {
         _currentPointToMove++;

         if (_currentPointToMove>=pointsToMove.Length)
         {
            _currentPointToMove = 0;
         }
      }
   }
}
