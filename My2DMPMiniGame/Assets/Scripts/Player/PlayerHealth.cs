using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class PlayerHealth : MonoBehaviour
{
    [SerializeField] private int healthCountForFirstPlayer = 3,healthCountForSecondPlayer=3;
    private int _maxHealth;

    [SerializeField] private Slider firstPlayerSlider,secondHealthSlider;

    private int _firstPlayerIndex = 1, _secondPlayerIndex = 2;
    
    private void Start()
    {
            firstPlayerSlider.maxValue = healthCountForFirstPlayer;
            firstPlayerSlider.value = healthCountForFirstPlayer;

            secondHealthSlider.maxValue = healthCountForSecondPlayer;
            secondHealthSlider.value = healthCountForSecondPlayer;

            _maxHealth = healthCountForFirstPlayer;
    }

    public void FirstPlayerTakeDamage(int damage)
    {
        healthCountForFirstPlayer -= damage;
        print(healthCountForFirstPlayer);
        
        SoundManager.Instance.PlaySFX(2);

        if (healthCountForFirstPlayer<=0)
        {
            print("Pomer");
            SoundManager.Instance.PlaySFX(3);
            GetComponent<PlayerController>().PlayDeadAnimation();
            UIManager.Instance.WinGame(_secondPlayerIndex);
            GameManager.Instance.gameFinished = true;
        }

        firstPlayerSlider.value = healthCountForFirstPlayer;
    }
    
    public void SecondPlayerTakeDamage(int damage)
    {
        healthCountForSecondPlayer -= damage;
        print(healthCountForFirstPlayer + "2");
        
        SoundManager.Instance.PlaySFX(2);

        if (healthCountForSecondPlayer<=0)
        {
            print("Pomer2");
            SoundManager.Instance.PlaySFX(3);
            GetComponent<PlayerController>().PlayDeadAnimation();
            UIManager.Instance.WinGame(_firstPlayerIndex);
            GameManager.Instance.gameFinished = true;
        }

        secondHealthSlider.value = healthCountForSecondPlayer;
    }

    public void HealFirstPlayer(int heal)
    {
        if (healthCountForFirstPlayer>=_maxHealth)
        {
            healthCountForFirstPlayer = _maxHealth;
        }
        else
        {
            healthCountForFirstPlayer+=heal;
            firstPlayerSlider.value = healthCountForFirstPlayer;
        }
    }

    public void HealSecondPlayer(int heal)
    {
        if (healthCountForSecondPlayer>=_maxHealth)
        {
            healthCountForSecondPlayer = _maxHealth;
        }
        else
        {
            healthCountForSecondPlayer += heal;
            secondHealthSlider.value = healthCountForSecondPlayer;
        }
    }

}
