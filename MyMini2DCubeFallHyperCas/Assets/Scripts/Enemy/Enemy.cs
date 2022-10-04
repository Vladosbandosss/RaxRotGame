using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Enemy : MonoBehaviour
{
    [SerializeField] private float maxYPos = 6f;

    private void Update()
    {
        CheckForDie();
    }

    private void CheckForDie()
    {
        if (transform.position.y>=maxYPos)
        {
            Destroy(gameObject);
        }
    }

    private void OnCollisionEnter2D(Collision2D col)
    {
        if (col.gameObject.CompareTag(TagManager.PLAYER_TAG))
        {
            GameManager.Instance.GameOver();
        }
    }
}
