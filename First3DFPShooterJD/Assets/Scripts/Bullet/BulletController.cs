using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class BulletController : MonoBehaviour
{
    private Rigidbody _rb;
    
    [SerializeField] private float moveSpeed=15f;
    [SerializeField] private float lifeTime=5f;

    [SerializeField] private GameObject impactFX;

    [SerializeField] private int damage = 1;

    [SerializeField] private bool damageEnemy, damagePlayer;

    private void Awake()
    {
        _rb = GetComponent<Rigidbody>();
    }

    private void Start()
    {
        Destroy(gameObject,lifeTime);
    }

    private void Update()
    {
        Movement();
    }

    private void Movement()
    {
        _rb.velocity=transform.forward*moveSpeed;
    }

    private void OnTriggerEnter(Collider other)
    {
        if(other.CompareTag(TagManager.ENEMY_TAG)&&damageEnemy)
        {
            other.GetComponent<EnemyHealth>().DamageEnemy(damage);
        }

        if (other.CompareTag(TagManager.PLAYER_TAG)&&damagePlayer)
        {
            PlayerHealth.Instance.DamagePlayer(damage);
        }
        
        Instantiate(impactFX, transform.position, transform.rotation);
        Destroy(gameObject);
    }
}
