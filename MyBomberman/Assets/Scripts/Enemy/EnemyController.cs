using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class EnemyController : MonoBehaviour
{
    private Rigidbody _rigidbody;
    private Animator _anim;

    [SerializeField] private float moveSpeed;
    
    [SerializeField] private Transform [] targets;
    private int _indexToMove = 0;
    private float _distanceToChangeDirection = 0.1f;
    private bool _isMovingForward = true;

    private bool _isMoving = true;

    [SerializeField] private GameObject deadFX;
    [SerializeField] private Transform deadSpawnPosition;

    private bool _canMove = true;
    [SerializeField] private float timeBetweenDeadAndDissepearEnemy = 1f;

    private void Awake()
    {
        _rigidbody = GetComponent<Rigidbody>();
        _anim = GetComponent<Animator>();
    }
    
    private void FixedUpdate()
    {
        if (_canMove)
        {
            EnemyPatrolling();
        }
    }

    private void EnemyPatrolling()
    {
        if (_isMoving)
        {
            _rigidbody.MovePosition(Vector3.MoveTowards
                (transform.position,targets[_indexToMove].position,Time.deltaTime*moveSpeed));
            
            transform.LookAt(targets[_indexToMove].position);

            if (Vector3.Distance(transform.position,targets[_indexToMove].position)<_distanceToChangeDirection)
            {
                if (_isMovingForward)
                {
                    if (_indexToMove>=targets.Length-1)
                    {
                        _isMovingForward = false;
                        _indexToMove--;
                    }
                    else
                    {
                        _indexToMove++;
                    }
                }
                else
                {
                    if (_indexToMove<=0)
                    {
                        _isMovingForward = true;
                        _indexToMove++;
                    }
                    else
                    {
                        _indexToMove--;
                    }
                }
            }
        }
    }

    public void Die()
    {
        _canMove = false;
        
        AudioManager.Instance.EnemyDeadSFX();
        
        Instantiate(deadFX, deadSpawnPosition.position, Quaternion.identity);
        _anim.SetTrigger(TagManager.ENEMY_DEAD_TRIGGER);
        
        Invoke("KillEnemy",timeBetweenDeadAndDissepearEnemy);
    }

    private void OnCollisionEnter(Collision collision)
    {
        if (collision.gameObject.CompareTag(TagManager.PLAYER_TAG))
        {
            _isMoving = false;
        }

        if (collision.gameObject.CompareTag(TagManager.BOMB_TAG))
        {
            if (_isMovingForward)
            {
                _indexToMove--;
            }
            else
            {
                _indexToMove++;
            }
        }
    }

    private void KillEnemy()
    {
        Destroy(gameObject);
    }
}
