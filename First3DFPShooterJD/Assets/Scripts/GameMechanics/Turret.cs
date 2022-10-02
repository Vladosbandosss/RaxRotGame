using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Turret : MonoBehaviour
{
    [SerializeField] private GameObject bullet;
    [SerializeField] private float rangeToTargetPlayer,timeBetweenShoots=1f;
    private float _shootCounter;

    [SerializeField] private Transform turret;

    [SerializeField] private Transform[] firePoints;

    private Vector3 _offsetToPlayer = new Vector3(0f, 1.2f, 0f);

    [SerializeField] private float rotationSpeed = 10f;

    private void Update()
    {
        Check();
    }

    private void Check()
    {
        if (Vector3.Distance(transform.position,PlayerController.Instance.transform.position)<rangeToTargetPlayer)
        {
            turret.LookAt(PlayerController.Instance.transform.position+_offsetToPlayer);

            if (Time.time>_shootCounter)
            {
                Instantiate(bullet, firePoints[0].position, firePoints[0].rotation);
                Instantiate(bullet, firePoints[1].position, firePoints[1].rotation);

                _shootCounter = Time.time + timeBetweenShoots;
            }
        }
        else
        {
            turret.rotation=Quaternion.Lerp(turret.rotation,
                Quaternion.Euler(0f,turret.rotation.eulerAngles.y+rotationSpeed,0f),rotationSpeed*Time.deltaTime);
        }
    }
}
