using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class EagleController : MonoBehaviour
{

    private SpriteRenderer _sr;
    
    [SerializeField] private Transform[] pointsToFly;
    [SerializeField] private float moveSpeed = 3f, chaseSpeed = 4f;
    private int _currentPoint;

    [SerializeField] private float distanceToAttackPlayer = 8f;

    private void Awake()
    {
        _sr = GetComponent<SpriteRenderer>();
    }

    private void Start()
    {
        for (int i = 0; i < pointsToFly.Length; i++)
        {
            pointsToFly[i].parent = null;
        }
    }

    private void Update()
    {
        Eagle();
    }

    private void Eagle()
    {
        if (Vector3.Distance(transform.position,PlayerController.instnace.transform.position)>distanceToAttackPlayer)
        {
          FlyPatrol();
        }
        else
        {
            ChasePlayer();
        }
    }

    private void FlyPatrol()
    {
        transform.position = Vector3.MoveTowards(transform.position, pointsToFly[_currentPoint].position,
            moveSpeed * Time.deltaTime);

        if (Vector3.Distance(transform.position,pointsToFly[_currentPoint].position)<0.1f)
        {
            _currentPoint++;

            if (_currentPoint>=pointsToFly.Length)
            {
                _currentPoint = 0;
            }
        }

        if (transform.position.x<pointsToFly[_currentPoint].position.x)
        {
            _sr.flipX = true;
        }
        else
        {
            _sr.flipX = false;
        }
    }

    private void ChasePlayer()
    {
        transform.position = Vector3.MoveTowards(transform.position, PlayerController.instnace.transform.position,
            chaseSpeed * Time.deltaTime);
    }
}
