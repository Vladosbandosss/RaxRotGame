using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerController : MonoBehaviour
{

    public static PlayerController instnace;
    
    private Rigidbody2D _rb;
    private Animator _anim;

    [SerializeField] private float moveSpeed = 7.5f;
    private float _hAxis;

    [SerializeField] private Transform groundCheckPosition;
    [SerializeField] private LayerMask groundLayer;
    [SerializeField] private float checkGroundRadius = 0.1f;
    [SerializeField] private float jumpForce = 15f;

    //[SerializeField] private float bounceForce = 30f;
    private bool _canDoubleJump;
    private bool _isGrounded;

    private Vector3 _temp;

    [SerializeField] private float knockForce = 10f, knockTime = 1f;
    private bool _canMove;

    [SerializeField] private float bounceForce = 25f;

    public bool stopInput;

    [SerializeField] private float bouncePaddleForce = 40f;

    private void Awake()
    {
        if (instnace==null)
        {
            instnace = this;
        }
        
        _rb = GetComponent<Rigidbody2D>();
        _anim = GetComponent<Animator>();

        _canMove = true;
    }

    private void Update()
    {
        if (_canMove&&!stopInput)
        {
            MovePlayer();

            IsGrounded();
        
            FacingDirection();
        
            AnimatePlayer();
        }
    }

    private void MovePlayer()
    {
        _hAxis = Input.GetAxisRaw(TagManager.HORIZONTAL_AXIS);
        _rb.velocity = new Vector2(_hAxis * moveSpeed, _rb.velocity.y);

        if (Input.GetKeyDown(KeyCode.Space))
        {
            Jump();
        }
    }

    private void Jump()
    {
        if (_isGrounded)
        {
            _rb.velocity = new Vector2(_rb.velocity.x, jumpForce);
            _canDoubleJump = true;
        }else if (_canDoubleJump)
        {
            _rb.velocity = new Vector2(_rb.velocity.x, jumpForce);
            _canDoubleJump = false;
        }
    }

    private bool IsGrounded()
    {
        return _isGrounded =
            Physics2D.Raycast(groundCheckPosition.position, Vector2.down, checkGroundRadius, groundLayer);
    }

    private void FacingDirection()
    {
        _temp = transform.localScale;

        if (_hAxis>0)
        {
            _temp.x = 1f;
        }else if (_hAxis<0)
        {
            _temp.x = -1f;
        }

        transform.localScale = _temp;
    }

    private void AnimatePlayer()
    {
        _anim.SetFloat(TagManager.SPEED_PLAYER_ANIMATION_PARAMETR,Mathf.Abs(_rb.velocity.x));
        _anim.SetBool(TagManager.ISGROUNDED_PLAYER_ANIMATION,_isGrounded);
        _anim.SetFloat(TagManager.Y_VELOCITY_PLAYER_ANIMATION,_rb.velocity.y);
    }

    public void KnockBack()
    {
        StartCoroutine("Knock");
    }

    private IEnumerator Knock()
    {
        _canMove = false;
        
        _anim.SetBool(TagManager.HURT_PLAYER_ANIMATION_PARAMETR,true);

        if (transform.localScale.x==1f)
        {
            _rb.AddForce(new Vector2(-knockForce,knockForce),ForceMode2D.Impulse);
        }
        else
        {
            _rb.AddForce(new Vector2(knockForce,knockForce),ForceMode2D.Impulse);
        }
       
        yield return new WaitForSeconds(0.5f);
        
        _anim.SetBool(TagManager.HURT_PLAYER_ANIMATION_PARAMETR,false);
        
        _canMove = true;
    }

    public void Bounce()
    {
        _rb.velocity = new Vector2(_rb.velocity.x, bounceForce);
    }

    public void BouncePaddle()
    {
        _rb.velocity = new Vector2(_rb.velocity.x, bouncePaddleForce);
    }
    
}
 