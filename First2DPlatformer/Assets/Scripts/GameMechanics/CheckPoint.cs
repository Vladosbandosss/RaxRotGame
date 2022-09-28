using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CheckPoint : MonoBehaviour
{
    private SpriteRenderer _sr;
    [SerializeField] private Sprite cpOn, cpOff;

    private void Awake()
    {
        _sr = GetComponent<SpriteRenderer>();
    }

    private void OnTriggerEnter2D(Collider2D col)
    {
        if (col.CompareTag(TagManager.PLAYER_TAG))
        {
            CheckPointController.instance.DeactivateCheckPoints();
            
            _sr.sprite = cpOn;
            
            AudioManager.instance.PlaySFX(1);
            
            CheckPointController.instance.SetSpawnPoint(transform.position);
        }
    }

    public void ResetCheckPoint()
    {
        _sr.sprite = cpOff;
    }
}
