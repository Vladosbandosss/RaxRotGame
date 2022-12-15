using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using Random = UnityEngine.Random;

public class PowerUp : MonoBehaviour
{
    enum PowerUps
    {
        MaxBombs,
        Range,
        Speed
    }

    private PowerUps _powerUps;

    private void Start()
    {
        ChooserPower();
    }

    private void ChooserPower()
    {
        int powerIndex = Random.Range(0, 3);

        switch (powerIndex)
        {
            case 0:
                _powerUps = PowerUps.MaxBombs;
                break;
            case 1:
                _powerUps = PowerUps.Range;
                break;
            case 2:
                _powerUps = PowerUps.Speed;
                break;
            default:
                _powerUps = PowerUps.MaxBombs;
                break;
        }
    }

    private void OnTriggerEnter(Collider other)
    {
        if (other.CompareTag(TagManager.PLAYER_TAG))
        {
            if (_powerUps==PowerUps.MaxBombs)
            {
                PlayerController.Instance.IncreaseMaxBombs();
                
            }else if (_powerUps==PowerUps.Range)
            {
               GameManager.Instance.IncreaseRange();
               
            }else if (_powerUps==PowerUps.Speed)
            {
                PlayerController.Instance.IncreaseSpeed();
            }
            AudioManager.Instance.PowerPickUpSFX();
            
            PlayerController.Instance.PickUpItem();
            
            Destroy(gameObject);
        }
    }
}
