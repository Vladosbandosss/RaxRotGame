using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class FrogController : MonoBehaviour
{
    [SerializeField] private float moveSpeed=5f;

    [SerializeField] private Transform leftPoint, rightPoint;

    private bool _movingRight=true;

    private Rigidbody2D _rb;
    private Animator _anim;
    [SerializeField] private SpriteRenderer sr;

    [SerializeField] private float moveTime=3f, waitTime=1f;
    private float _moveCounter, _waitCounter;

    private void Awake()
    {
        _rb = GetComponent<Rigidbody2D>();
        _anim = GetComponent<Animator>();
    }

    private void Start()
    {
        leftPoint.parent = null;
        rightPoint.parent = null;
        //now this points dont move with frog but they attached to frog!

        _moveCounter = moveTime;
        
    }

    private void Update()
    {
        
        if (_moveCounter>0)
        {
            _moveCounter -= Time.deltaTime;
            
            Movement();

            if (_moveCounter<=0)
            {
                _waitCounter = waitTime;
            }
        }
        else if(_waitCounter>0)
        {
            _waitCounter -= Time.deltaTime;

            _rb.velocity = new Vector2(0f, _rb.velocity.y);
            
            _anim.SetBool(TagManager.FROG_MOVING_PARAMETR,false);

            if (_waitCounter<=0)
            {
                _moveCounter = moveTime;
            }
        }
    }

    private void Movement()
    {
        if (_movingRight)
        {
            sr.flipX = true;
            
            _rb.velocity = new Vector2(moveSpeed, _rb.velocity.y);

            if (transform.position.x>rightPoint.position.x)
            {
                _movingRight = !_movingRight;
            }
        }
        else
        {
            sr.flipX = false;
            
            _rb.velocity = new Vector2(-moveSpeed, _rb.velocity.y);
            
            if (transform.position.x<leftPoint.position.x)
            {
                _movingRight = !_movingRight;
            }
        }
        
        _anim.SetBool(TagManager.FROG_MOVING_PARAMETR,true);
    }
}
