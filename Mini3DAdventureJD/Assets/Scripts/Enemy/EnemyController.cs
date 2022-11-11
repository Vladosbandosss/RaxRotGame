using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.AI;

public class EnemyController : MonoBehaviour
{
    private NavMeshAgent _agent;
    private Animator _anim;
    
    [SerializeField] private Transform[] patrolPoints;
    private int _currentPatrolIndex;

    private Transform _target;

    [SerializeField] private float timerToRepeatAttack = 5f;
    private float _attackTimer;

    private bool _canAttack;

    [SerializeField] private GameObject enemyDeadFX;
    [SerializeField] private Transform enemyDeadFXSpawnPos;

    private bool _isAlive;
    
    public enum EnemyState
    {
        Patrolling,
        Chasing,
        Attack
    }
    public EnemyState enemyState;
    
    
    private void Awake()
    {
        _agent = GetComponent<NavMeshAgent>();
        _anim = GetComponent<Animator>();

        enemyState = EnemyState.Chasing;

        _target = FindObjectOfType<PlayerController>().transform;

        _isAlive = true;
    }

    private void Update()
    {
        
        if (_isAlive)
        {
            FindState();
       
            CheckForAttack();
        }
    }

    private void FindState()
    {
        switch (enemyState)
        {
            case EnemyState.Chasing:
                
                FollowPlayer();
                
                break;
            case EnemyState.Patrolling:
                
                Patrol();
                
                break;
            case EnemyState.Attack:
                
                Attack();
                
                break;
        }
    }

    private void Patrol()
    {
        _agent.SetDestination(patrolPoints[_currentPatrolIndex].position);
        
        _anim.SetBool(TagManager.ENEMY_IS_HUNTING_ANIMATION,false);
        

        if (Vector3.Distance(transform.position,patrolPoints[_currentPatrolIndex].position)<1f)
        {
            _currentPatrolIndex++;
            if (_currentPatrolIndex>=patrolPoints.Length)
            {
                _currentPatrolIndex = 0;
            }
        }

        if (Vector3.Distance(transform.position,_target.position)<5f)
        {
            
            enemyState = EnemyState.Chasing;
        }
    }

    private void FollowPlayer()
    {
        _agent.SetDestination(_target.position);
        
        _anim.SetBool(TagManager.ENEMY_IS_HUNTING_ANIMATION,true);
        
        if (Vector3.Distance(transform.position,_target.position)>6f)
        {
            enemyState = EnemyState.Patrolling;
        }

        if (Vector3.Distance(transform.position,_target.position)<1f)
        {
            enemyState = EnemyState.Attack;
        }
    }

    private void Attack()
    {
        if (_canAttack)
        {
            PlayerHealthController.Instance.DamagePlayer();
            _canAttack = false;
            _attackTimer = Time.time + timerToRepeatAttack;
        }
        
        if (Vector3.Distance(transform.position,_target.position)>1f)
        {
            enemyState = EnemyState.Chasing;
        }
    }

    private void CheckForAttack()
    {
        if (Time.time>_attackTimer)
        {
            _canAttack = true;
        }
    }

    private void OnTriggerEnter(Collider other)
    {
        if (other.CompareTag(TagManager.PLAYER_TAG)&&_isAlive)
        {
            _isAlive = false;
            
            _anim.SetTrigger(TagManager.ENEMY_DEAD_TRIGGER);
            
            Instantiate(enemyDeadFX, enemyDeadFXSpawnPos.position, Quaternion.identity);
        }
    }

    private void DestroyEnemy()
    {
        Destroy(gameObject);
    }
}
