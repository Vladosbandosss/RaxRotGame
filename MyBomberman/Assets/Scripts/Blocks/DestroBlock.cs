using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using Random = UnityEngine.Random;

public class DestroBlock : MonoBehaviour
{

    private Animator _anim;
    
    [SerializeField] private GameObject destroParticleFX;
    [SerializeField] private Transform destroParticlePosition;

    [SerializeField] private GameObject powerUpObj;
    [SerializeField] private Transform powerUpSpawnPos;

    private void Awake()
    {
        _anim = GetComponent<Animator>();
    }

    public void PlayParticleFX()
    {
        MakePower();
        Instantiate(destroParticleFX, destroParticlePosition.position, Quaternion.identity);
    }

    public void DestroyDestroBlock()
    {
        _anim.SetTrigger(TagManager.DESTRY_BLOCK_ANIM_TRIGGER);
    }

    private void DestroyBlock()
    {
        Destroy(gameObject);
    }

    private void MakePower()
    {
        if (Random.Range(0,100)>75)
        {
            AudioManager.Instance.powerSpawnSFX();
            
            Instantiate(powerUpObj, transform.position, Quaternion.identity);
        }
    }
}
