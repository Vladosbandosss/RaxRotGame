using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerHealth : MonoBehaviour
{
    private SpriteRenderer _sr;
    
    public static PlayerHealth instance;
    
    [HideInInspector]public int currentHealth, maxHealth=3;

    [SerializeField] private float invincibleLength=2f;
    private float _invincibleCounter;

    [SerializeField] private float normalAlpha = 1f, halfAlpha = 0.5f;

    [SerializeField] private GameObject deadFX;

    private void Awake()
    {
        if (instance==null)
        {
            instance = this;
        }

        _sr = GetComponent<SpriteRenderer>();
    }

    private void Update()
    {
        if (_invincibleCounter>0)
        {
            _invincibleCounter -= Time.deltaTime;

            if (_invincibleCounter<=0)
            {
                _sr.color = new Color(_sr.color.r, _sr.color.g, _sr.color.b,normalAlpha);
            }
        }
    }

    private void Start()
    {
        currentHealth = maxHealth;
    }

    public void DealDamage()
    {
        if (_invincibleCounter<=0)
        {
            currentHealth--;
        
            UIController.instance.DecreaseHealth();

            if (currentHealth<=0)
            {
                Instantiate(deadFX, transform.position, Quaternion.identity);
                
                LevelManager.instance.RespawnPlayer();
                
                AudioManager.instance.PlaySFX(8);
            }
            else
            {
                _invincibleCounter = invincibleLength;
                _sr.color = new Color(_sr.color.r, _sr.color.g, _sr.color.b,halfAlpha);
                
                AudioManager.instance.PlaySFX(9);
                
                PlayerController.instnace.KnockBack();
            }
        }
    }

    public void HealPlayer()
    {
        currentHealth++;
        
        if (currentHealth>maxHealth)
        {
            currentHealth = maxHealth;
        }
        
        UIController.instance.IncreaseHealth();
        
    }
}
