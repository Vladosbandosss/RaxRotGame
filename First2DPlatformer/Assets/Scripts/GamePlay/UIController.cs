using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class UIController : MonoBehaviour
{
    public static UIController instance;

    [SerializeField] private Image[] heartImages;
    private int _currentIndexHealth;

    [SerializeField] private Text gemTXT;
    private int _initialGemCount;

    [SerializeField] private Image fadeScreen;
    [SerializeField] private float fadeSpeed = 3f;
    private bool _fadeToBlack, _fadeFromBlack;

    [SerializeField] private GameObject levelEndText;

    private void Awake()
    {
        if (instance==null)
        {
            instance = this;
        }

        _currentIndexHealth = heartImages.Length - 1;

        gemTXT.text = _initialGemCount.ToString();
    }

    private void Update()
    {
        MakeFade();
    }

    private void MakeFade()
    {
        if (_fadeToBlack)
        {
            fadeScreen.color = new Color(fadeScreen.color.r, fadeScreen.color.b, fadeScreen.color.g,
                Mathf.MoveTowards(fadeScreen.color.a, 1f, fadeSpeed*Time.deltaTime));

            if (fadeScreen.color.a==1f)
            {
                _fadeFromBlack = false;
            }
        }

        if (_fadeFromBlack)
        {
            fadeScreen.color = new Color(fadeScreen.color.r, fadeScreen.color.b, fadeScreen.color.g,
                Mathf.MoveTowards(fadeScreen.color.a, 0f, fadeSpeed*Time.deltaTime));

            if (fadeScreen.color.a==0f)
            {
                _fadeFromBlack = false;
            }
        }
    }

    public void FadeToBlack()
    {
        _fadeToBlack = true;
        _fadeFromBlack = false;
    }

    public void FadeFromBlack()
    {
        _fadeToBlack = false;
        _fadeFromBlack = true;
    }

    public void DecreaseHealth()
    {
        heartImages[_currentIndexHealth].gameObject.SetActive(false);

        _currentIndexHealth--;

        if (_currentIndexHealth<0)
        {
            _currentIndexHealth = 0;
        }
    }

    public void IncreaseHealth()
    {
        _currentIndexHealth++;
        
        if (_currentIndexHealth==heartImages.Length)
        {
            _currentIndexHealth = heartImages.Length - 1;
        }
        
        heartImages[_currentIndexHealth].gameObject.SetActive(true);
    }

    public void SetInitialHealth()
    {
        for (int i = 0; i < heartImages.Length; i++)
        {
            heartImages[i].gameObject.SetActive(true);
        }
        _currentIndexHealth = heartImages.Length - 1;
    }

    public void GemCollected()
    {
        gemTXT.text = LevelManager.instance.gemsCollected.ToString();
    }

    public void ShowLevelCompleteTxt()
    {
        levelEndText.SetActive(true);
    }
    
    
}
