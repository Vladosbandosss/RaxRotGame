using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Crank : MonoBehaviour
{
    private Animator _anim;

    private void Awake()
    {
        _anim = GetComponent<Animator>();
    }

    private void OnTriggerEnter2D(Collider2D col)
    {
        if (col.CompareTag(TagManager.PLAYER_TAG))
        {
            _anim.SetTrigger(TagManager.PUSHED_CRANK_TRIGGER);
            
            Door.instance.OpenDoor();
            
        }
    }
}
