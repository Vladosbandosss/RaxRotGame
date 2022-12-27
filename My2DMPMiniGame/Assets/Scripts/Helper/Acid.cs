using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Acid : MonoBehaviour
{
    [SerializeField] private int damage = 5;
    [SerializeField] private GameObject acidFX;
    private void OnCollisionEnter2D(Collision2D col)
    {
        if (col.gameObject.CompareTag(TagManager.FIRST_PLAYER_TAG))
        {
            col.gameObject.GetComponent<PlayerHealth>().FirstPlayerTakeDamage(damage);
            Instantiate(acidFX, col.gameObject.transform.position, Quaternion.identity);
        }

        if (col.gameObject.CompareTag(TagManager.SECOND_PLAYER_TAG))
        {
            col.gameObject.GetComponent<PlayerHealth>().SecondPlayerTakeDamage(damage);
            Instantiate(acidFX, col.gameObject.transform.position, Quaternion.identity);
        }
    }
}
