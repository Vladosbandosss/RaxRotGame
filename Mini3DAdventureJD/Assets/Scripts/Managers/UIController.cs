using System;
using System.Collections;
using System.Collections.Generic;
using TMPro;
using UnityEngine;
using UnityEngine.SceneManagement;
using UnityEngine.UI;

public class UIController : MonoBehaviour
{
    public static UIController Instance;

    [SerializeField] private Image fadeImage;
    private bool _isFadingToBlack, _isFadingFromBlack;
    [SerializeField] private float fadeSpeed = 2f;

    [SerializeField] private Slider healthSlider;
    [SerializeField] private TMP_Text healthText;

    [SerializeField] private TMP_Text coinText;
    private int _coinCount;

    [SerializeField] private GameObject pausePanel;
    
    private void Awake()
    {
        if (Instance==null)
        {
            Instance = this;
        }
    }

    private void Start()
    {
        coinText.text = _coinCount.ToString();
        
        pausePanel.SetActive(false);
    }

    private void Update()
    {
        MakeFadeOrNot();

        if (Input.GetKeyDown(KeyCode.Escape))
        {
          
        }
    }

    private void MakeFadeOrNot()
    {
        if (_isFadingToBlack)
        {
            fadeImage.color = new Color(fadeImage.color.r, fadeImage.color.g, fadeImage.color.b,
                Mathf.MoveTowards(fadeImage.color.a, 1f, fadeSpeed*Time.deltaTime));
        }

        if (_isFadingFromBlack)
        {
            fadeImage.color = new Color(fadeImage.color.r, fadeImage.color.g, fadeImage.color.b,
                Mathf.MoveTowards(fadeImage.color.a, 0f, fadeSpeed*Time.deltaTime));
        }

        if (Input.GetKeyDown(KeyCode.Escape))
        {
            ShowPausePanel();
        }
       
    }

    public void FadeToBlack()
    {
        _isFadingToBlack = true;
        _isFadingFromBlack = false;
    }

    public void FadeFromBlack()
    {
        _isFadingFromBlack = true;
        _isFadingToBlack = false;
    }

    public void UpdateHealthDisplay(int currentHealth)
    {
        healthText.text = "Health: " + currentHealth + "/" + PlayerHealthController.Instance.maxHealth;

        healthSlider.maxValue = PlayerHealthController.Instance.maxHealth;
        healthSlider.value = currentHealth;
    }

    public void AddCoin()
    {
        _coinCount++;
        coinText.text = _coinCount.ToString();
    }

    private void ShowPausePanel()
    {
        Cursor.lockState = CursorLockMode.None;
        pausePanel.SetActive(true);
        Time.timeScale = 0f;
    }

    public void UnPaused()
    {
        Cursor.lockState = CursorLockMode.Locked;
        pausePanel.SetActive(false);
        Time.timeScale = 1f;
    }

    public void LevelSelect()
    {
        Time.timeScale = 1f;
        SceneManager.LoadScene(TagManager.LEVEL_SELECT_SCENE_NAME);
    }

    public void GoToMainMenu()
    {
        Time.timeScale = 1f;
        SceneManager.LoadScene(TagManager.MAIN_MENU_SCENE_NAME);
    }
}
