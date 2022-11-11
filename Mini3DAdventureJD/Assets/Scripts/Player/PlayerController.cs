using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerController : MonoBehaviour
{
    private CharacterController _characterController;
    private CameraController _cameraController;

    public Animator anim;
    
    [SerializeField] private float moveSpeed;
    
    private Vector3 _moveVector;
    private float _zAxis;
    private float _xAxis;

    private Vector3 _moveAmount;

    private float _yStore;

    [SerializeField] private float jumpForce,gravityScale;

    [SerializeField] private float rotateSpeed=10f;

    [SerializeField] private GameObject jumpFX;

    [HideInInspector]
    public bool stopMoving;

    private void Awake()
    {
        _characterController = GetComponent<CharacterController>();
        _cameraController = FindObjectOfType<CameraController>();
    }

    private void Update()
    {
        if (!stopMoving)
        {
            PlayerMovement();
        }
        
        AnimatePlayer();
    }

    private void FixedUpdate()
    {
        if (!_characterController.isGrounded)
        {
            _moveAmount.y +=Physics.gravity.y*gravityScale*Time.deltaTime;
        }
        else
        {
            _moveAmount.y =Physics.gravity.y*gravityScale*Time.deltaTime;
        }
    }

    private void PlayerMovement()
    {
        _yStore = _moveAmount.y;
        
        _moveAmount = _cameraController.transform.forward * Input.GetAxisRaw(TagManager.Z_AXIS)+
                      _cameraController.transform.right*Input.GetAxisRaw(TagManager.X_AXIS);
        _moveAmount.y = 0f;
        _moveAmount = _moveAmount.normalized;

        if (_moveAmount.magnitude>0.1f)
        {
            if (_moveAmount!=Vector3.zero)
            {
                Quaternion newRot = Quaternion.LookRotation(_moveAmount);
                
                transform.rotation=Quaternion.Slerp(transform.rotation,newRot,rotateSpeed*Time.deltaTime);
            }
        }

        _moveAmount.y = _yStore;

        if (_characterController.isGrounded)
        {
            
            if (Input.GetKeyDown(KeyCode.Space))
            {
                _moveAmount.y = jumpForce;
                
                jumpFX.SetActive(true);
            }
        }

        _characterController.Move(new Vector3(_moveAmount.x*moveSpeed,_moveAmount.y,_moveAmount.z*moveSpeed)*Time.deltaTime);
    }

    private void AnimatePlayer()
    {
        float moveVel = new Vector3(_moveAmount.x, 0f, _moveAmount.z).magnitude * moveSpeed;
        
        anim.SetFloat(TagManager.SPEED_PLAYER_PARAMETR,moveVel);
        anim.SetBool(TagManager.IS_GROUNDED_PLAYER_PARAMETR,_characterController.isGrounded);
        anim.SetFloat(TagManager.Y_VELOCITY_PLAYER_PARAMETR,_moveAmount.y);
    }
}
