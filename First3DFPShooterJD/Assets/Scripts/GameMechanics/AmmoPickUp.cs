using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class AmmoPickUp : MonoBehaviour
{
    private bool _collected;
    
    private void OnTriggerEnter(Collider other)
    {
        if (other.CompareTag(TagManager.PLAYER_TAG)&&!_collected)
        {
            PlayerController.Instance.activeGun.GetAmmo();
            
            AudioManager.Instance.PlaySFX(8);
            
            _collected = true;
            
            Destroy(gameObject);
        }
    }
}
