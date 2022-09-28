using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using Random = UnityEngine.Random;

public class StompBox : MonoBehaviour
{
    [SerializeField] private GameObject deadFX;

    [SerializeField] private GameObject [] collectables;
    private int _spawnIndex;
    private bool _canSpawnCollectables;

    private void Start()
    {
        SpawnCollectables();
    }

    private void SpawnCollectables()
    {
        if (Random.Range(0,10)>5)
        {
            _canSpawnCollectables = true;
            _spawnIndex = Random.Range(0, collectables.Length);
        }
    }

    private void OnTriggerEnter2D(Collider2D col)
    {
        if (col.CompareTag(TagManager.ENEMY_TAG))
        {
            Instantiate(deadFX, col.transform.position, Quaternion.identity);
            
            Destroy(col.transform.parent.gameObject);
            
            PlayerController.instnace.Bounce();
            
            AudioManager.instance.PlaySFX(3);

            if (_canSpawnCollectables)
            {
                Instantiate(collectables[_spawnIndex], col.transform.position, Quaternion.identity);
            }
        }
    }
    
}
