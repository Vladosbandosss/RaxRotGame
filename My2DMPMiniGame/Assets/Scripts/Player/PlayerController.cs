using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerController : MonoBehaviour
{

    private Rigidbody2D _rb;
    private Animator _anim;

    [SerializeField] private float moveSpeed =8.5f, jumpForce=21f;
    private float _xAxis;

    private bool _isGrounded;
    [SerializeField] private Transform groundCheckPoint;
    [SerializeField] private LayerMask ground;
    [SerializeField] private float distanceToGroundCheck = 0.1f;

    [SerializeField]private bool isFirstPlayer, isSecondPlayer;

    [SerializeField] private GameObject knifeWeapon;
    [SerializeField] private Transform knifeShootPosition;
    [SerializeField] private float timeBetweenKnifeShoots = 0.25f;
    private float _timerBetweenFirstPlayer,_timerBetweenSecondPlayer;
    
    [SerializeField] private RectTransform firstHealthBarTransform, secondBarTransform;
    private Vector3 _firstHealthBarTempScale, _secondHealthBarTempScale;
    
    private void Awake()
    {
        _rb = GetComponent<Rigidbody2D>();
        _anim = GetComponent<Animator>();
        
    }

    private void Start()
    {
        SpawnPlayer();
    }

    private IEnumerator _SpawnPlayer()
    {
        GameManager.Instance.SpawnPlayerFX(transform.position);
        yield return new WaitForSeconds(0.2f);
    }

    private void SpawnPlayer()
    {
        StartCoroutine(nameof(_SpawnPlayer));
    }

    private void Update()
    {
        if (GameManager.Instance.canPlayGame  && !GameManager.Instance.gameFinished)
        {
            Game();
        }
    }

    private void Game()
    {
        PlayerMovement();
        
        JumpPlayer();
        
        AnimatePlayer();
        
        Attack();
    }

    private void PlayerMovement()
    {
        //Test();

        if (isFirstPlayer)
        {
            if (Input.GetKey(KeyCode.D))
            {
                _xAxis = 1f;
            }else if (Input.GetKey(KeyCode.A))
            {
                _xAxis = -1f;
            }
            else
            {
                _xAxis = 0;
            }
            _rb.velocity = new Vector2(_xAxis * moveSpeed, _rb.velocity.y);
        }

        if (isSecondPlayer)
        {
            if (Input.GetKey(KeyCode.RightArrow))
            {
                _xAxis = 1f;
            }else if (Input.GetKey(KeyCode.LeftArrow))
            {
                _xAxis = -1f;
            }
            else
            {
                _xAxis = 0;
            }
            _rb.velocity = new Vector2(_xAxis * moveSpeed, _rb.velocity.y);
        }

        ChosePlayerDirection();
    }

    private void Test()
    {
        if (isFirstPlayer)
        {
            _xAxis = Input.GetAxisRaw(TagManager.HORIZONTAL_AXIS);
            _rb.velocity = new Vector2(_xAxis * moveSpeed, _rb.velocity.y);
        }

        if (isSecondPlayer)
        {
            _xAxis = Input.GetAxisRaw(TagManager.HORIZONTAL_AXIS);
            _rb.velocity = new Vector2(_xAxis * moveSpeed, _rb.velocity.y);
        }
    }

    private void JumpPlayer()
    {
        _isGrounded = Physics2D.Raycast(groundCheckPoint.position, Vector2.down, distanceToGroundCheck, ground);
        
        
        if (Input.GetKeyDown(KeyCode.W) && _isGrounded && isFirstPlayer)
        {
            _rb.velocity = new Vector2(_rb.velocity.x, jumpForce);
            
            SoundManager.Instance.PlaySFX(1);
        }

        if (Input.GetKeyDown(KeyCode.UpArrow)&& _isGrounded && isSecondPlayer)
        {
            _rb.velocity = new Vector2(_rb.velocity.x, jumpForce);
            
            SoundManager.Instance.PlaySFX(1);
        }
    }

    private void Attack()
    {
        if (isFirstPlayer)
        {
            if (Input.GetKeyDown(KeyCode.S))
            {
                if (Time.time>_timerBetweenFirstPlayer)
                {
                    _anim.SetTrigger(TagManager.KNIFE_ATTACK_TRIGGER);
                    
                    SoundManager.Instance.PlaySFX(0);
                    
                    GameObject knife = Instantiate(knifeWeapon, knifeShootPosition.position, Quaternion.identity);
                    knife.GetComponent<Knife>().SenKnifeMovement(transform.localScale.x);
                    
                    ResetFirstPlayerMovement();

                    _timerBetweenFirstPlayer = Time.time + timeBetweenKnifeShoots;
                }
            }
        }

        if (isSecondPlayer)
        {
            if (Input.GetKeyDown(KeyCode.DownArrow))
            {
                if (Time.time>_timerBetweenSecondPlayer)
                {
                    _anim.SetTrigger(TagManager.KNIFE_ATTACK_TRIGGER);
                    
                    SoundManager.Instance.PlaySFX(0);
                    
                    GameObject knife = Instantiate(knifeWeapon, knifeShootPosition.position, Quaternion.identity);
                    knife.GetComponent<Knife>().SenKnifeMovement(transform.localScale.x);
                    
                    ResetSecondPlayerMovement();

                    _timerBetweenSecondPlayer = Time.time + timeBetweenKnifeShoots;
                }
            }
        }
    }

   

    private void ResetFirstPlayerMovement()
    {
        _rb.velocity = new Vector2(0f, _rb.velocity.y);
    }

    private void ResetSecondPlayerMovement()
    {
        _rb.velocity = new Vector2(0f, _rb.velocity.y);
    }
    
    private void AnimatePlayer()
    {
        _anim.SetBool(TagManager.PLAYER_IS_GROUND_PARAMETR,_isGrounded);
        _anim.SetFloat(TagManager.PLAYER_SPEED_PARAMETR,Math.Abs(_rb.velocity.x));
        _anim.SetFloat(TagManager.PLAYER_VELOCITY_Y_PARAMETR,_rb.velocity.y);
    }

    private void ChosePlayerDirection()
    {
        if (_xAxis>0)
        {
            transform.localScale = new Vector3(1f, 1f, 1f);
        }else if (_xAxis<0)
        {
            transform.localScale = new Vector3(-1f, 1f, 1f);
        }

        if (isFirstPlayer)
        {
            _firstHealthBarTempScale = firstHealthBarTransform.localScale;
            if (transform.localScale.x>0f)
            {
                _firstHealthBarTempScale.x = Math.Abs(_firstHealthBarTempScale.x);
            }
            else
            {
                _firstHealthBarTempScale.x = -Math.Abs(_firstHealthBarTempScale.x);
            }

            firstHealthBarTransform.localScale = _firstHealthBarTempScale;
        }

        if (isSecondPlayer)
        {
            _secondHealthBarTempScale = secondBarTransform.localScale;
            if (transform.localScale.x>0f)
            {
                _secondHealthBarTempScale.x = Math.Abs(_secondHealthBarTempScale.x);
            }
            else
            {
                _secondHealthBarTempScale.x = -Math.Abs(_secondHealthBarTempScale.x);
            }

            secondBarTransform.localScale = _secondHealthBarTempScale;
        }
        
    }

    public void PlayDeadAnimation()
    {
        _anim.Play(TagManager.PLAY_DEAD_ANIM);
        _rb.velocity=Vector2.zero;
    }
    
}
