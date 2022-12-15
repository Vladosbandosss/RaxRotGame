using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Explosion : MonoBehaviour
{
    private Rigidbody _rigidbody;
    private Vector3 _explodeDirection=Vector3.zero;
    private float _explodeSpeed = 4f;
    private float _explodeRange = 3f;

    [SerializeField] private int scoreForDeadEnemy = 5;

    private void Awake()
    {
        _rigidbody = GetComponent<Rigidbody>();
    }

    private void Start()
    {
        Invoke(nameof(DestroyExplosion),_explodeRange);
    }

    private void FixedUpdate()
    {
        _rigidbody.velocity = _explodeDirection*_explodeSpeed;
    }

    public void SetMoveVector(Vector3 moveDirection,float speed,float range)
    {
        _explodeDirection = moveDirection;
        _explodeSpeed = speed;
        _explodeRange = range;
    }

    private void DestroyExplosion()
    {
        Destroy(gameObject);
    }

    private void OnTriggerEnter(Collider other)
    {
        if (other.CompareTag(TagManager.BLOCK_TAG))
        {
            DestroyExplosion();
        }

        if (other.CompareTag(TagManager.BOMB_TAG))
        {
            other.gameObject.GetComponent<Bomb>().Explode();
            DestroyExplosion();
        }

        if (other.gameObject.CompareTag(TagManager.PLAYER_TAG))
        {
            other.gameObject.GetComponent<PlayerController>().Die();
            DestroyExplosion();
        }

        if (other.gameObject.CompareTag(TagManager.ENEMY_TAG))
        {
            GameManager.Instance.EnemyDead();
            other.gameObject.GetComponent<EnemyController>().Die();
            DestroyExplosion();
            
           GameManager.Instance.UpdateScore(scoreForDeadEnemy);
        }

        if (other.gameObject.CompareTag(TagManager.DESTROYABLE_BLOCK_TAG))
        {
            other.gameObject.GetComponent<DestroBlock>().PlayParticleFX();
            other.gameObject.GetComponent<DestroBlock>().DestroyDestroBlock();
            DestroyExplosion();
        }
    }
}
