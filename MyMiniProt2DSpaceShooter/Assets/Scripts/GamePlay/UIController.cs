using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class UIController : MonoBehaviour
{
    public static UIController Instance;

    public Slider playerHealthSlider;

    private void Awake()
    {
        if (Instance==null)
        {
            Instance = this;
        }
    }
    
}
