using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Serialization;

public class PlayerController : MonoBehaviour
{
    public static PlayerController Instance;
    
    private CharacterController _characterController;
    private Animator _anim;

    [SerializeField] private float moveSpeed = 8f;

    private Vector3 _moveInput;
    private Vector3 _verticalMove, _horizontalMove;
    
    [SerializeField] private Transform camPoint;

    private Vector2 _mouseInput;
    [SerializeField] private float mouseSensitivity = 1.5f;

    [SerializeField] private float gravityModifier=2f;

    [SerializeField] private float jumpPower = 10f, doubleJumpPower = 5f;
    private bool _canJump;
    private bool _canDoubleJump;
    [SerializeField] private Transform groundCheckPoint;
    [SerializeField] private LayerMask groundLayer;
    [SerializeField] private float distanceGroundCheck = 0.3f;

    [SerializeField] private float runSpeed = 12f;
    
    [SerializeField] private Transform bulletFirePosition;

    public Gun activeGun;
    [SerializeField] private List<Gun> allGuns = new List<Gun>();
    private int _currentGun;

    private float _bounceAmount;
    private bool _bounce;
    
    private void Awake()
    {
        if (Instance==null)
        {
            Instance = this;
        }
        
        _characterController = GetComponent<CharacterController>();
        _anim = GetComponent<Animator>();
    }

    private void Start()
    {
       SwitchGun();
    }

    private void Update()
    {
        MakeGame();
    }

    private void MakeGame()
    {
        if (!UIController.Instance.pauseScreen.activeInHierarchy&&!GameManager.Instance.loadingNextLevel)
        {
            Movement();

            CanJump();
        
            AnimatePlayer();
        }
    }

    private void Movement()
    {
        float yStore = _moveInput.y;
        
        _verticalMove = transform.forward * Input.GetAxisRaw(TagManager.Z_INPUT_AXIS);
        _horizontalMove = transform.right * Input.GetAxisRaw(TagManager.X_INPUT_AXIS);

        _moveInput = _verticalMove + _horizontalMove;
        _moveInput.Normalize();
        
        if (Input.GetKey(KeyCode.LeftShift))
        {
            _moveInput *= runSpeed;
            
           // AudioManager.Instance.PlaySFX(5);
        }
        else
        {
            _moveInput *= moveSpeed;
            
           // AudioManager.Instance.StopSFX(5);
            
            //AudioManager.Instance.PlaySFX(5);
        }

        _moveInput.y = yStore;
        _moveInput.y += Physics.gravity.y * gravityModifier*Time.deltaTime;

        if (_characterController.isGrounded)
        {
            _moveInput.y = Physics.gravity.y*gravityModifier*Time.deltaTime;
        }

        if (Input.GetKeyDown(KeyCode.Space)&&_canJump)
        {
            _moveInput.y = jumpPower;
            _canDoubleJump = true;
            
            AudioManager.Instance.PlaySFX(12);
        }else if (Input.GetKeyDown(KeyCode.Space)&&_canDoubleJump)
        {
            _moveInput.y = doubleJumpPower;
            _canDoubleJump = false;
            
            AudioManager.Instance.PlaySFX(12);
        }

        if (_bounce)
        {
            _bounce = false;
            _moveInput.y = _bounceAmount;
            _canDoubleJump = true;
        }

        _characterController.Move(_moveInput*Time.deltaTime);
        
        Rotation();

        if (Input.GetMouseButtonDown(0))
        {
            Shoot();
        }

        if (Input.GetKeyDown(KeyCode.Q))
        {
            SwitchGun();
        }
    }

    private void Rotation()
    {
        _mouseInput = new Vector2(Input.GetAxisRaw(TagManager.X_INPUT_MOUSE),
            Input.GetAxisRaw(TagManager.Y_INPUT_MOUSE))*mouseSensitivity;
        
        transform.rotation=Quaternion.Euler(transform.rotation.eulerAngles+new Vector3(0f,_mouseInput.x,0f));
        
        camPoint.rotation=Quaternion.Euler(camPoint.rotation.eulerAngles+new Vector3(-_mouseInput.y,0f,0f));
    }

    private bool CanJump()
    {
        return _canJump = Physics.Raycast(groundCheckPoint.position, Vector3.down, distanceGroundCheck, groundLayer);
    }

    private void AnimatePlayer()
    {
        _anim.SetFloat(TagManager.PLAYER_ANIM_MOVE_SPEED_PARAMETR,_moveInput.magnitude);
        _anim.SetBool(TagManager.PLAYER_ANIM_ISGROUNDED_PARAMETR,_canJump);
    }

    private void Shoot()
    {
        RaycastHit hit;
        if (Physics.Raycast(camPoint.position,camPoint.forward,out hit,50f))
        {
            bulletFirePosition.LookAt(hit.point);
        }
        
        FireShoot();
    }

    private void FireShoot()
    {
        if (activeGun.currentAmmo>0)
        {
            activeGun.currentAmmo--;
            
            UIController.Instance.ammoTxt.text = activeGun.currentAmmo.ToString();

            if (_currentGun==0)
            {
                AudioManager.Instance.PlaySFX(6);
            }

            if (_currentGun==1)
            {
                AudioManager.Instance.PlaySFX(7);
            }
            
            Instantiate(activeGun.bullet,bulletFirePosition.position, bulletFirePosition.rotation);
        }
    }

    private void SwitchGun()
    {
        activeGun.gameObject.SetActive(false);
        _currentGun++;

        if (_currentGun>=allGuns.Count)
        {
            _currentGun = 0;
        }

        activeGun = allGuns[_currentGun];
        activeGun.gameObject.SetActive(true);

        UIController.Instance.ammoTxt.text = activeGun.currentAmmo.ToString();

        bulletFirePosition.position = activeGun.firePoint.position;
    }

    public void Bounce(float bounceForce)
    {
        _bounceAmount = bounceForce;
        _bounce = true;
    }
}
