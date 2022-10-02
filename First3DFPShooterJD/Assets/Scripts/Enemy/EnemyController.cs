using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.AI;

public class EnemyController : MonoBehaviour
{
    private NavMeshAgent _agent;
    
    private bool _chasing;
    [SerializeField] private float distanceToChase=10f,distanceToLose=15f;

    private Vector3 _targetPoint,_startPoint;

    [SerializeField] private GameObject bullet;
    [SerializeField] private Transform firePoint;
    [SerializeField] private float timeBetweenShoots = 2f;
    private float _shootCounter;

    [SerializeField] private Animator enemyAnim;
    
    private void Awake()
    {
        _agent = GetComponent<NavMeshAgent>();
    }

    private void Start()
    {
        _startPoint = transform.position;
    }

    private void Update()
    {
        Movement();
    }

    private void Movement()
    {

        _targetPoint = PlayerController.Instance.transform.position;
        _targetPoint.y = transform.position.y;
        
        if (!_chasing)
        {
            if (Vector3.Distance(transform.position,_startPoint)<0.1f)
            {
                enemyAnim.SetBool(TagManager.ENEMY_ANIM_ISMOVING_PARAMETR,false);
            }
            
            if (Vector3.Distance(transform.position,PlayerController.Instance.transform.position)<distanceToChase)
            {
                _chasing = true;

                _shootCounter = Time.time + timeBetweenShoots;
            }
        }
        else
        {
            _agent.destination = _targetPoint;
            
            enemyAnim.SetBool(TagManager.ENEMY_ANIM_ISMOVING_PARAMETR,true);

            if (Time.time>_shootCounter)
            {
                if (PlayerController.Instance.gameObject.activeInHierarchy)
                {
                    Shoot();

                    _shootCounter = Time.time + timeBetweenShoots;
                }
                else
                {
                    _agent.destination = _startPoint;
                }
            }
            
            if (Vector3.Distance(transform.position,PlayerController.Instance.transform.position)>distanceToLose)
            {
                _chasing = false;

                _agent.destination = _startPoint;
            }
        }
    }

    private void Shoot()
    {
        enemyAnim.SetTrigger(TagManager.ENEMY_ANIM_SHOOT_TRIGGER);
        
        Instantiate(bullet, firePoint.position, firePoint.rotation);
        
        AudioManager.Instance.PlaySFX(2);
    }
}
