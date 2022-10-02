using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class BouncePad : MonoBehaviour
{
    [SerializeField] private float bounceForce;

    private void OnTriggerEnter(Collider other)
    {
        if (other.CompareTag(TagManager.PLAYER_TAG))
        {
            PlayerController.Instance.Bounce(bounceForce);
            
            AudioManager.Instance.PlaySFX(0);
        }
    }
}
