using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class BouncePad : MonoBehaviour
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
            PlayerController.instnace.BouncePaddle();
            _anim.SetTrigger(TagManager.BOUNCER_TRIGGER);
        }
    }
}
