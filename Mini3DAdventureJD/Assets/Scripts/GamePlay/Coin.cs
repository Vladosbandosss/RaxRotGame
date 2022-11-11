using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Coin : MonoBehaviour
{
    [SerializeField] private GameObject coinFX;
    [SerializeField] private Transform coinFXPositionSpawn;
    private void OnTriggerEnter(Collider other)
    {
        if (other.CompareTag(TagManager.PLAYER_TAG))
        { 
            Instantiate(coinFX, coinFXPositionSpawn.position, Quaternion.identity);
            
            UIController.Instance.AddCoin();
            Destroy(gameObject);
        }
    }
}
