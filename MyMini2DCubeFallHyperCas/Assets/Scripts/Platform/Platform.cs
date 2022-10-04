using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Platform : MonoBehaviour
{
    private Animator _anim; 
    
    [SerializeField] private bool isPlatform, isBreakable, isSpike, isMovingLeft, isMovingRight;

    [SerializeField] private float moveSpeed = 2f;

    private Vector3 _temp;

    [SerializeField] private float maxY = 6f;

    [SerializeField] private float movePlatformSpeed = 1f;

    private void Awake()
    {
        if (isBreakable)
        {
            _anim = GetComponent<Animator>();
        }  
    }

    private void Update()
    {
        MovePlatform();
    }

    private void MovePlatform()
    {
        _temp = transform.position;
        _temp.y += moveSpeed * Time.deltaTime;
        transform.position = _temp;

        if (transform.position.y>=maxY)
        {
            Destroy(gameObject);
        }
    }

    private void OnTriggerEnter2D(Collider2D col)
    {
        if (col.CompareTag(TagManager.PLAYER_TAG))
        {
            GameManager.Instance.GameOver();
        }
    }

    private void OnCollisionEnter2D(Collision2D col)
    {
        if (col.gameObject.CompareTag(TagManager.PLAYER_TAG))
        {
            AudioController.Instance.PlayLandSfx();
            
            if (isBreakable)
            {
                _anim.Play(TagManager.PLAY_BREAK_ANIMATION);
            }
        }
    }

    private void OnCollisionStay2D(Collision2D collision)
    {
        if (collision.gameObject.CompareTag(TagManager.PLAYER_TAG))
        {
            if (isMovingLeft)
            {
                PlayerController.Instance.MovePlayerInMovingPlatform(-movePlatformSpeed);
            }

            if (isMovingRight)
            {
                PlayerController.Instance.MovePlayerInMovingPlatform(movePlatformSpeed);
            }
        }
    }

    private void DeactivateBreakablePlatform()
    {
        gameObject.SetActive(false);
    }
}
