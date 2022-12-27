using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using Random = UnityEngine.Random;

public class Platform : MonoBehaviour
{
    [SerializeField] private GameObject one_banana, bananas;

    [SerializeField] private Transform spawnPoint;

    [SerializeField] private GameObject bird;
    [SerializeField] private float birdXToSPawn = 1f;

    private void Start()
    {
        SpawnBanan();
        
        SpawnBird();
    }

    private void SpawnBanan()
    {
        GameObject newBanana = null;
        if (Random.Range(0,10)>3)
        {
            newBanana = Instantiate(one_banana, spawnPoint.position, Quaternion.identity);
        }
        else
        {
            newBanana = Instantiate(bananas, spawnPoint.position, Quaternion.identity);
        }

        newBanana.transform.parent = transform;
    }

    private void SpawnBird()
    {
        if (Random.Range(0,10)>7)
        {
            Vector2 temp = spawnPoint.position;
            if (Random.Range(0,2)==0)
            {
                temp.x += Random.Range(0, birdXToSPawn);
            }
            else
            {
                temp.x -= Random.Range(0, -birdXToSPawn);
            }

            Instantiate(bird, temp, Quaternion.identity);
        }
    }
}
