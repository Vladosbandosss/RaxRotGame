using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class UIController : MonoBehaviour
{
    public static UIController Instance;

    public Slider healthSlider;

    public Text ammoTxt;

    [SerializeField] private Image damageFX;
    [SerializeField] private float damageAlpha = 0.25f;
    [SerializeField] private float waitDamageTime = 0.15f;

    public GameObject pauseScreen;

    [SerializeField] private Image fadeScreen;
    [SerializeField] private float fadeSpeed = 1.5f;

    private void Awake()
    {
        if (Instance==null)
        {
            Instance = this;
        }
    }

    private void Start()
    {
        pauseScreen.SetActive(false);
    }

    private void Update()
    {
        Fade();
    }

    private void Fade()
    {
        if (!GameManager.Instance.loadingNextLevel)
        {
            fadeScreen.color = new Color(fadeScreen.color.r, fadeScreen.color.g, fadeScreen.color.b,
                Mathf.MoveTowards(fadeScreen.color.a, 0f, fadeSpeed * Time.deltaTime));
        }
        else
        {
            fadeScreen.color = new Color(fadeScreen.color.r, fadeScreen.color.g, fadeScreen.color.b,
                Mathf.MoveTowards(fadeScreen.color.a, 1f, fadeSpeed * Time.deltaTime));
        }
    }

    public void ShowDamage()
    {
        StartCoroutine(ShowDamageCO());
    }

    private IEnumerator ShowDamageCO()
    {
        damageFX.color = new Color(damageFX.color.r, damageFX.color.g, damageFX.color.b,damageAlpha);
        yield return new WaitForSeconds(waitDamageTime);
        damageFX.color = new Color(damageFX.color.r, damageFX.color.g, damageFX.color.b,0);
    }
    
}
