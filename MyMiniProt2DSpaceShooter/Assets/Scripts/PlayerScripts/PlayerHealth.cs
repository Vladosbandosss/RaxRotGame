using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerHealth : MonoBehaviour
{
    public static PlayerHealth Instance;
    private int _maxHealth = 100,_currentHealth;

    private void Awake()
    {
        if (Instance==null)
        {
            Instance = this;
        }
        
        _currentHealth = _maxHealth;
    }

    private void Start()
    {
        UIController.Instance.playerHealthSlider.maxValue = _maxHealth;
        UIController.Instance.playerHealthSlider.value = _currentHealth;
    }

    public void TakeDamage(int damage)
    {
        _currentHealth -= damage;
        UIController.Instance.playerHealthSlider.value = _currentHealth;
        AudioManager.Instance.PlayExploFX();

        if (_currentHealth<=0)
        {
            //GAME MANAGER
        }
    }
}
