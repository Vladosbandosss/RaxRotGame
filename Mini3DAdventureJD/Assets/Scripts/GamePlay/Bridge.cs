using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Bridge : MonoBehaviour
{
    public static Bridge Instance;

    [SerializeField] private GameObject showBridgeFX;
    [SerializeField] private Transform bridgeFxPos;

    private void Awake()
    {
        if (Instance==null)
        {
            Instance = this;
        }
    }

    private void Start()
    {
        gameObject.SetActive(false);
    }

    public void ShowFx()
    {
        gameObject.SetActive(true);
        
        Instantiate(showBridgeFX, bridgeFxPos.position, Quaternion.identity);
    }
}
