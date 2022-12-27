using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class BGSpawner : MonoBehaviour
{
    private GameObject[] bgs;
    private float _height;
    private float _highest_Y_Pos;

    private void Awake()
    {
        bgs = GameObject.FindGameObjectsWithTag(TagManager.BG_TAG); 
    }

    private void Start()
    {
        _height = bgs[0].GetComponent<BoxCollider2D>().bounds.size.y;
        _highest_Y_Pos = bgs[0].transform.position.y;

        for (int i = 1; i < bgs.Length; i++)
        {
            if (bgs[i].transform.position.y>_highest_Y_Pos)
            {
                _highest_Y_Pos = bgs[i].transform.position.y;
            }
        }
    }

    private void OnTriggerEnter2D(Collider2D col)
    {
        if (col.CompareTag(TagManager.BG_TAG))
        {
            if (col.transform.position.y>=_highest_Y_Pos)
            {
                Vector3 temp = col.transform.position;
                for (int i = 0; i < bgs.Length; i++)
                {
                    if (!bgs[i].activeInHierarchy)
                    {
                        temp.y += _height;
                        bgs[i].transform.position = temp;
                        bgs[i].gameObject.SetActive(true);

                        _highest_Y_Pos = temp.y;
                    }
                }
            }
        }
    }
}
