using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Serialization;

public class PlayerController : MonoBehaviour
{
    public static PlayerController Instance;
    
    private Rigidbody _rigidbody;
    private Animator _anim;
    
    [SerializeField] private float xMoveSpeed=3;
    [SerializeField] private float zMoveSpeed=3;

    [SerializeField] private float maxMoveSpeed = 5;

    [SerializeField] private float increaseSpeedPower = 0.5f;

    private Vector3 _moveNewPosition;
    private float _xAxis;
    private float _zAxis;

    [SerializeField] private GameObject bombPrefab;

    private GameManager _myGameManager;

    [SerializeField] private int maxBombs = 1;
    private int _currentBombPlaced = 0;
    [SerializeField] private int maxBombsPlayerCanHave = 5;

    [SerializeField] private float timeBetweenSpawnBomb =0.3f;
    private float _timerBetweenSpawnBomb;
    
    [HideInInspector]public bool hasControl = true;

    private bool _isMoving;

    private void Awake()
    {
        if (Instance==null)
        {
            Instance = this;
        }
        
        _rigidbody = GetComponent<Rigidbody>();
        _anim = GetComponent<Animator>();
        
        _myGameManager = FindObjectOfType<GameManager>();
    }

    private void Start()
    {
        UIManager.Instance.UpdateBombs(maxBombs);
        UIManager.Instance.UpdateSpeed(xMoveSpeed);
    }

    void Update()
    {
        if (hasControl&&!UIManager.Instance.isPaused)
        {
            Movement();
            
            Rotation();
        
            PlaceBomb();
            
            AnimatePlayer();
        }
        else
        {
            _rigidbody.velocity=Vector3.zero;
        }
    }

    private void Movement()
    {
        _xAxis = Input.GetAxisRaw(TagManager.X_MOVEMENT_AXIS);
        _zAxis = Input.GetAxisRaw(TagManager.Z_MOVEMENT_AXIS);

        _moveNewPosition = new Vector3(_xAxis * xMoveSpeed, 0f, _zAxis * zMoveSpeed);
        _rigidbody.velocity = _moveNewPosition;
    }

    private void PlaceBomb()
    {
        if (Input.GetKeyDown(KeyCode.Space)&&_currentBombPlaced<maxBombs)
        {
            if (Time.time>_timerBetweenSpawnBomb)
            {
                AudioManager.Instance.BombPlacedSFX();
                
                GameObject bomb = Instantiate(bombPrefab, transform.position, Quaternion.identity);
                _currentBombPlaced++;

                _timerBetweenSpawnBomb = Time.time + timeBetweenSpawnBomb;
            }
        } 
    }

    private void Rotation()
    {
        if (_rigidbody.velocity!=Vector3.zero)
        {
            transform.forward = _rigidbody.velocity;
        }
    }

    public void Die()
    {
        _myGameManager.PlayerDied();
    }

    private void OnCollisionEnter(Collision collision)
    {
        if (collision.gameObject.CompareTag(TagManager.ENEMY_TAG))
        {
            Die();
        }
    }

    public void BombExploded()
    {
        _currentBombPlaced--;
        
        if (_currentBombPlaced<0)
        {
            _currentBombPlaced = 0;
        }
    }

    public void IncreaseMaxBombs()
    {
        maxBombs++;
        
        if (maxBombs>maxBombsPlayerCanHave)
        {
            maxBombs = maxBombsPlayerCanHave;
        }
        
        UIManager.Instance.UpdateBombs(maxBombs);
    }

    public void IncreaseSpeed()
    {
        xMoveSpeed+=increaseSpeedPower;
        zMoveSpeed+=increaseSpeedPower;

        if (xMoveSpeed>maxMoveSpeed||zMoveSpeed>maxMoveSpeed)
        {
            xMoveSpeed = maxMoveSpeed;
            zMoveSpeed = maxMoveSpeed;
        }
        
        UIManager.Instance.UpdateSpeed(xMoveSpeed);
    }

    private void AnimatePlayer()
    {
        if (_xAxis!=0||_zAxis!=0)
        {
            _isMoving = true;
        }
        else
        {
            _isMoving = false;
        }
        
        _anim.SetBool(TagManager.PLAYER_MOVE_ANIM_PARAMETR,_isMoving);
    }

    public void PickUpItem()
    {
        _anim.SetTrigger(TagManager.PLAYER_PICKUP_TRIGGER);
    }
}
