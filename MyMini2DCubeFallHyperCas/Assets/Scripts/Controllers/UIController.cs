using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class UIController : MonoBehaviour
{
    public static UIController Instance;
    
    [SerializeField] private Image fadeImage;
    [HideInInspector] public bool makeDarkFade, makeNormalFade;
    [SerializeField] private float fadeSpeed = 2f;

    [SerializeField] private GameObject resumePanel;
    private bool _isPaused;

    [SerializeField] private Text scoreTxt;
    private int _scoreCounter;

    [SerializeField] private float scoreUpdateTimer = 2f;
    private float _scoreUpdateCounter;
    

    private void Awake()
    {
        if (Instance==null)
        {
            Instance = this;
        }

        scoreTxt.text=_scoreCounter.ToString();

        _scoreUpdateCounter = scoreUpdateTimer;
    }

    private void Update()
    {
        MakeFade();
        
        CheckForPause();
        
        UpdateScore();
    }

    private void CheckForPause()
    {
        if (Input.GetKeyDown(KeyCode.Escape)&&!_isPaused)
        {
            _isPaused = true;
            Time.timeScale = 0f;
            
            resumePanel.SetActive(true);
        }
    }

    private void MakeFade()
    {
        if (makeNormalFade)
        {
            fadeImage.color = new Color(fadeImage.color.r,fadeImage.color.g,fadeImage.color.b,
                Mathf.MoveTowards(fadeImage.color.a,0f,fadeSpeed*Time.deltaTime));

            if (fadeImage.color.a==0f)
            {
                makeNormalFade = false;
            }
        }

        if (makeDarkFade)
        {
            fadeImage.color = new Color(fadeImage.color.r,fadeImage.color.g,fadeImage.color.b,
                Mathf.MoveTowards(fadeImage.color.a,1f,fadeSpeed*Time.deltaTime));

            if (fadeImage.color.a==1f)
            {
                makeDarkFade = false;
            }
        }
    }

    private void UpdateScore()
    {
        if (Time.time>_scoreUpdateCounter)
        {
            _scoreCounter++;
            scoreTxt.text=_scoreCounter.ToString();

            _scoreUpdateCounter = Time.time + scoreUpdateTimer;
        }
    }

    public void ResumeGame()
    {
        resumePanel.SetActive(false);
        Time.timeScale = 1f;
        _isPaused = false;
    }

    public void QuitGame()
    {
        Application.Quit();
    }
}
