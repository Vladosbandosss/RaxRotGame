using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Heal : MonoBehaviour
{
    [SerializeField] private int healPoint = 1;

    [SerializeField] private GameObject healFX;
    private void OnTriggerEnter2D(Collider2D col)
    {
        if (col.CompareTag(TagManager.FIRST_PLAYER_TAG))
        {
            Instantiate(healFX, transform.position, Quaternion.identity);
            col.GetComponent<PlayerHealth>().HealFirstPlayer(healPoint);
            SoundManager.Instance.PlaySFX(4);
            Destroy(gameObject);
        }

        if (col.CompareTag(TagManager.SECOND_PLAYER_TAG))
        {
            Instantiate(healFX, transform.position, Quaternion.identity);
            col.GetComponent<PlayerHealth>().HealSecondPlayer(healPoint);
            SoundManager.Instance.PlaySFX(4);
            Destroy(gameObject);
        }
    }
}
