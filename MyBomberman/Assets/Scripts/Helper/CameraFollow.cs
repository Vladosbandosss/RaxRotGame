using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CameraFollow : MonoBehaviour
{ 
    private Transform followTarget;
    private Vector3 offset=new Vector3(0f,20f,0f);
    private bool _canFollowTatget = false;
    
    private void Start()
    {
        Invoke("FindTarget",3f);
    }

    private void LateUpdate()
    {
        if (_canFollowTatget)
        {
            transform.position = followTarget.position+offset;
        }
    }

    public void FindTarget()
    {
        followTarget = GameObject.FindWithTag(TagManager.PLAYER_TAG).transform;
        _canFollowTatget = true;
    }
}
