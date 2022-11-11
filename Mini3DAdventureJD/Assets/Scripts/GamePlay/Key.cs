using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Key : MonoBehaviour
{
    [SerializeField] private Transform effectPos;
    [SerializeField] private GameObject fx;

    private void Start()
    {
        //Bridge.Instance.gameObject.SetActive(false);
    }


    private void OnTriggerEnter(Collider other)
    {
        if (other.CompareTag(TagManager.PLAYER_TAG))
        {
            Instantiate(fx, effectPos.position, Quaternion.identity);
            
            
           // Bridge.Instance.gameObject.SetActive(true);
            Bridge.Instance.ShowFx();
            
            Destroy(gameObject);
        }
    }
}
