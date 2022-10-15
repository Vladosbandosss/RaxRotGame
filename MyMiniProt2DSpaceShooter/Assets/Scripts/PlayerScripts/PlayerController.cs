using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerController : MonoBehaviour
{
    [SerializeField] private float minXPos = -2.5f, maxXPos=2.5f;
    [SerializeField] private float minYPos = -4.5f, maxYPos = -2f;

    [SerializeField] private float moveSpeed = 3f;
    private float _xAxis, _yAxis;

    private Vector3 _temp;

    [SerializeField] private GameObject bullet;
    [SerializeField] private Transform bulletShootPosition;
    [SerializeField] private float shootTimer = 0.5f;
    private float _shootCounter;
    
    private void Update()
    {
        MovePlayer();
    }

    private void MovePlayer()
    {
        _xAxis = Input.GetAxis(TagManager.HORIZONTAL_INPUT)*moveSpeed*Time.deltaTime;
        _yAxis = Input.GetAxis(TagManager.VERTICAL_INPUT)*moveSpeed*Time.deltaTime;

        _temp=transform.position;
        
        _temp.x = Mathf.Clamp(_temp.x, minXPos, maxXPos);
        _temp.y = Mathf.Clamp(_temp.y, minYPos, maxYPos);
        
        _temp += new Vector3(_xAxis, _yAxis, 0f);
        
        transform.position = _temp;

        if (Input.GetKeyDown(KeyCode.Space))
        {
            Shoot();
        }
    }

    private void Shoot()
    {
        if (Time.time>_shootCounter)
        {
            Instantiate(bullet, bulletShootPosition.position, bullet.transform.rotation);
            
            AudioManager.Instance.PlayShootFX();
            
            _shootCounter = Time.time + shootTimer;
        }
    }
}
