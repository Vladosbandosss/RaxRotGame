using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class HealthPuckUp : MonoBehaviour
{
    [SerializeField] private int healAmount = 10;
    
    private void OnTriggerEnter(Collider other)
    {
        if (other.CompareTag(TagManager.PLAYER_TAG)
            &&PlayerHealth.Instance.currentHealth<PlayerHealth.Instance.maxHealth)
        {
            PlayerHealth.Instance.HealPlayer(healAmount);
            
            AudioManager.Instance.PlaySFX(9);
            
            Destroy(gameObject);
        }
    }
}
