using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Tank : MonoBehaviour
{
    [SerializeField] private float shootTimer = 2f;
    private float _shootCounter;

    [SerializeField] private GameObject bullet;
    [SerializeField] private Transform shootPosition;

    private void Update()
    {
        if (Time.time>_shootCounter)
        {
            Shoot();

            _shootCounter = Time.time + shootTimer;
        }
    }

    private void Shoot()
    {
        Instantiate(bullet, shootPosition.position, Quaternion.identity);
        
        AudioManager.instance.PlaySFX(2);
    }
}
