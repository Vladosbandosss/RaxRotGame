using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class BGScrolling : MonoBehaviour
{
   private Material _bgScroll;
   
   [SerializeField] private float scrollSpeed = 0.1f;
   private Vector2 _offset;

   private void Awake()
   {
      _bgScroll = GetComponent<Renderer>().material;
   }

   private void Update()
   {
      ScrollBG();
   }

   private void ScrollBG()
   {
      _offset = new Vector2(scrollSpeed*Time.deltaTime, 0f);
      _bgScroll.mainTextureOffset += _offset;
   }
}
