using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class BossTankController : MonoBehaviour
{
    public enum BossStates
    {
        Shooting,
        Hurt,
        Moving
    }
    public BossStates currentState;
    
    [SerializeField] private Transform boss;
    [SerializeField] private Animator anim;
    
    [Header("Movement")]
    [SerializeField] private float moveSpeed;
    [SerializeField] private Transform leftPoint, rightPoint;
    private bool _moveRight;
    
    [Header("Shooting")]
    [SerializeField] private GameObject bullet;
    [SerializeField] private Transform bulletShootPoint;
    [SerializeField] private float timeBetweenShoots;
    private float _shootCounter;
    
    [Header("Hurt")]
    [SerializeField] private float hurtTime;
    private float _hurtCounter;

    private void Start()
    {
        currentState = BossStates.Shooting;
    }

    private void Update()
    {
        BossStatesChecking();
    }

    private void BossStatesChecking()
    {
        switch (currentState)
        {
            case BossStates.Shooting:
                
                break;
            
            case BossStates.Hurt:
                
                HurtState();
                
                break;
            
            case BossStates.Moving:
                
                MoveState();
                
                break;
        }
    }

    public void TakeHit()
    {
        currentState = BossStates.Hurt;
        _hurtCounter = hurtTime;
        
        anim.SetTrigger(TagManager.BOSS_HIT_TRIGGER);
    }

    private void HurtState()
    {
        if (_hurtCounter>0)
        {
            hurtTime -= Time.deltaTime;

            if (_hurtCounter<=0)
            {
                currentState = BossStates.Moving;
            }
        }
    }

    private void MoveState()
    {
        if (_moveRight)
        {
            boss.position += new Vector3(moveSpeed * Time.deltaTime, 0f,0f);

            if (boss.position.x>rightPoint.position.x)
            {
                boss.localScale = new Vector3(1f, 1f, 1f);
                
                _moveRight = false;

               StopMovement();
            }
        }
        else
        {
            boss.position -= new Vector3(moveSpeed * Time.deltaTime, 0f,0f);

            if (boss.position.x<leftPoint.position.x)
            {
                boss.localScale = new Vector3(-1f, 1f, 1f);
                
                _moveRight = true;

               StopMovement();
            }
        }
    }

    private void StopMovement()
    {
        currentState = BossStates.Shooting;

        _shootCounter = timeBetweenShoots;
                
        anim.SetTrigger(TagManager.BOSS_STOP_MOVING);
    }
}
