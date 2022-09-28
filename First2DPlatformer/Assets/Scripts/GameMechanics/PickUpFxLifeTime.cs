using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PickUpFxLifeTime : MonoBehaviour
{
    [SerializeField] private float lifeTime = 0.5f;
    private void Start()
    {
        DestroyFx();
    }

    private void DestroyFx()
    {
        Destroy(gameObject,lifeTime);
    }
}
