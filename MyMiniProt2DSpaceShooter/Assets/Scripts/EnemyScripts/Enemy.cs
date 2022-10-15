using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Enemy : MonoBehaviour
{
    [SerializeField] private GameObject explossion;

    [SerializeField] private GameObject bullet;
    [SerializeField] private Transform shootPosition;
    [SerializeField] private float shootTimer = 2f;
    private float _shootCounter;

    [SerializeField] private float moveSpeed = 1f;

    [SerializeField] private int damageToPlayer = 20;

    private void Start()
    {
        _shootCounter = shootTimer;
    }

    private void Update()
    {
        EnemyMovement();
    }

    private void EnemyMovement()
    {
        transform.position -= new Vector3(0f, moveSpeed * Time.deltaTime, 0f);

        if (Time.time>_shootCounter)
        {
            Shoot();
            _shootCounter = Time.time + _shootCounter;
        }
    }

    private void Shoot()
    {
        Instantiate(bullet, shootPosition.position, bullet.transform.rotation);
        
        AudioManager.Instance.PlayShootFX();
    }

    public void DestroyEnemyShip()
    {
        Instantiate(explossion, transform.position, Quaternion.identity);
        AudioManager.Instance.PlayExploFX();
        Destroy(gameObject);
    }
    
    private void OnTriggerEnter2D(Collider2D col)
    {
        if (col.CompareTag(TagManager.PLAYER_TAG))
        {
            PlayerHealth.Instance.TakeDamage(damageToPlayer);

            DestroyEnemyShip();
        }
    }
}
