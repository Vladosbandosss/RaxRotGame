using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class BGScroll : MonoBehaviour
{
    private Material _bgMat;

    [SerializeField] private float scrollSpeed = 0.5f;

    private void Awake()
    {
        _bgMat = GetComponent<Renderer>().material;
    }

    private void Update()
    {
        Scroll();
    }

    private void Scroll()
    {
        Vector2 offset = new Vector2(0f, scrollSpeed * Time.deltaTime);
        _bgMat.mainTextureOffset += offset;
    }
}
