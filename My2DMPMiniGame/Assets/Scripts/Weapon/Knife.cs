using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Knife : MonoBehaviour
{
    private Rigidbody2D _rb;

    [SerializeField] private float moveKnifeSpeed = 9f;

    [SerializeField] private int knifeDamage = 1;

    [SerializeField] private float knifeLifeTime = 5f;

    [SerializeField] private GameObject knifeDestroyFX;
    
    [SerializeField] private GameObject playerDamageFX;

    private void Awake()
    {
        _rb = GetComponent<Rigidbody2D>();
    }

    private void Start()
    {
        Destroy(gameObject,knifeLifeTime);
    }

    private void Update()
    {
        MoveKnife();
    }

    private void MoveKnife()
    {
        _rb.velocity = new Vector2(moveKnifeSpeed, 0f);
    }

    public void SenKnifeMovement(float xLocalScale)
    {
        if (xLocalScale==1)
        {
            moveKnifeSpeed = moveKnifeSpeed;
        }
        else
        {
            moveKnifeSpeed = -moveKnifeSpeed;
            transform.localScale = new Vector3(-1f, 1f, 1f);
        }
    }

    private void OnCollisionEnter2D(Collision2D col)
    {
        if (col.gameObject.CompareTag(TagManager.FIRST_PLAYER_TAG))
        {
            col.gameObject.GetComponent<PlayerHealth>().FirstPlayerTakeDamage(knifeDamage);

            Instantiate(playerDamageFX, col.transform.position, Quaternion.identity);
            
            Destroy(gameObject);
        }
        else if (col.gameObject.CompareTag(TagManager.SECOND_PLAYER_TAG))
        {
            col.gameObject.GetComponent<PlayerHealth>().SecondPlayerTakeDamage(knifeDamage);
            
            Instantiate(playerDamageFX, col.transform.position, Quaternion.identity);
            
            Destroy(gameObject);
        }else if (col.gameObject.CompareTag(TagManager.KNIFE_TAG))
        {
            Instantiate(knifeDestroyFX, transform.position, Quaternion.identity);
            Destroy(col.gameObject);
            Destroy(gameObject);
        }
        else
        {
            Instantiate(knifeDestroyFX, transform.position, Quaternion.identity);
            
            Destroy(gameObject);
        }
    }
}
