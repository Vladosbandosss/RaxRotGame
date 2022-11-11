using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerHealthController : MonoBehaviour
{
    public static PlayerHealthController Instance;

    private int _currentHealth;
    public int maxHealth;

    private bool _canBeDamaged = true;
    [SerializeField] private float timeToResetDamage = 1f;
    [SerializeField] private GameObject flameBoy;

    private void Awake()
    {
        if (Instance==null)
        {
            Instance = this;
        }
    }

    private void Start()
    {
        FillHealth();
        
        flameBoy.gameObject.SetActive(_canBeDamaged);
    }

    public void DamagePlayer()
    {
        if (_canBeDamaged)
        {
            _currentHealth--;
            
            if (_currentHealth<=0)
            {
                LevelManager.Instance.RespawnPlayer();
            }
            else
            {
                StartCoroutine(ResetDamagePlayer());
            }
            
            UIController.Instance.UpdateHealthDisplay(_currentHealth);
        }
    }

    private IEnumerator ResetDamagePlayer()
    {
        _canBeDamaged = false;
        bool canBeShowed=false;

        for (int i = 0; i < 10; i++)
        {
            flameBoy.gameObject.SetActive(canBeShowed);
            yield return new WaitForSeconds(0.1f);
            canBeShowed = !canBeShowed;
        } 
        canBeShowed = true;
        flameBoy.gameObject.SetActive(canBeShowed);

        yield return new WaitForSeconds(timeToResetDamage);

        _canBeDamaged = true;
    }

    public void FillHealth()
    {
        _currentHealth = maxHealth;
        
        UIController.Instance.UpdateHealthDisplay(_currentHealth);
    }
}
