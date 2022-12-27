using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CameraFollow : MonoBehaviour
{
    private Transform _target;

    private bool _isFollowPlayer;

    [SerializeField] private float minYtreshold = -2.5f;

    private void Awake()
    {
        _target = GameObject.FindGameObjectWithTag(TagManager.PLAYER_TAG).transform;
    }

    private void Update()
    {
        if (!_target)
        {
            return;
        }
        
        Follow();
    }

    private void Follow()
    {
        if (_target.position.y<(transform.position.y-minYtreshold))
        {
            _isFollowPlayer = false;
        }

        if (_target.position.y>transform.position.y)
        {
            _isFollowPlayer = true;
        }

        if (_isFollowPlayer)
        {
            Vector3 temp = transform.position;
            temp.y = _target.position.y;
            transform.position = temp;
        }
    }
}
