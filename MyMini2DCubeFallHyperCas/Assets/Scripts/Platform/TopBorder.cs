using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class TopBorder : MonoBehaviour
{
    private void OnTriggerEnter2D(Collider2D col)
    {
        if (col.CompareTag(TagManager.PLAYER_TAG))
        {
            GameManager.Instance.GameOver();
        }
    }
}
