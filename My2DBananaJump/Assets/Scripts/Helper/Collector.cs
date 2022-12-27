using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Collector : MonoBehaviour
{
    private void OnTriggerEnter2D(Collider2D col)
    {
        if (col.CompareTag(TagManager.BG_TAG)|| col.CompareTag(TagManager.PLATFORM_TAG)
            || col.CompareTag(TagManager.PUSH_TAG) || col.CompareTag(TagManager.EXTRA_PUSH_TAG)
            || col.CompareTag(TagManager.BIRD_TAG))
        {
            col.gameObject.SetActive(false);
        }
    }
}
