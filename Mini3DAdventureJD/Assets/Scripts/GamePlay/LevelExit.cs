using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class LevelExit : MonoBehaviour
{
    private Animator _anim;
    
    [SerializeField] private string levelToLoad;

    private void Awake()
    {
        _anim = GetComponent<Animator>();
    }

    private void OnTriggerEnter(Collider other)
    {
        if (other.CompareTag(TagManager.PLAYER_TAG))
        {
           LevelManager.Instance.LoadNextLevel(levelToLoad);
        }
    }
}
