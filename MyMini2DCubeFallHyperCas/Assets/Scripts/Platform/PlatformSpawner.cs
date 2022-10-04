using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using Random = UnityEngine.Random;

public class PlatformSpawner : MonoBehaviour
{
    [SerializeField]
    private GameObject platform, breakablePlatform, spikePlatform, movingLeftPlatform, movingRightPlatform;

    [SerializeField] private float platformSpawnTimer = 2f;
    private float _spawnerCounterTime;
    private int _spawnedPlatformCounter=0;

    [SerializeField] private float minXToSPawnPos = -2.1f, maxXToSpawnPos = 2.1f;
    private Vector2 _spawnPos;
    private GameObject _newPlatform;

    [SerializeField] private GameObject[] enemies;
    private GameObject _spawnedEnemy;
    private float _enemyYOffset = 1.5f;
    private Vector2 _spawnEnemyPos;

    private void Start()
    {
        _spawnPos = transform.position;
    }

    private void Update()
    {
        if (Time.time>_spawnerCounterTime)
        {
            SpawnPlatform();
            
            _spawnerCounterTime = Time.time + platformSpawnTimer;
        }
    }

    private void SpawnPlatform()
    {
        _spawnPos.x = Random.Range(minXToSPawnPos, maxXToSpawnPos);
        _newPlatform = null;
        _spawnedEnemy = null;
        
        if (_spawnedPlatformCounter==0)
        {
            _newPlatform = Instantiate(platform, _spawnPos, Quaternion.identity);

            if (Random.Range(0,100)>50)
            {
                _spawnedEnemy=Instantiate(enemies[Random.Range(0, enemies.Length)], _newPlatform.transform.position,
                    Quaternion.identity);
                _spawnEnemyPos = _newPlatform.transform.position;
                _spawnEnemyPos.y += _enemyYOffset;
                _spawnedEnemy.transform.position = _spawnEnemyPos;
            }
        } if (_spawnedPlatformCounter==1)
        {
            if (Random.Range(0,100)>50)
            {
                _newPlatform = Instantiate(platform, _spawnPos, Quaternion.identity);
            }
            else
            {
                _newPlatform = Instantiate(breakablePlatform, _spawnPos, Quaternion.identity);
            }
        } if (_spawnedPlatformCounter==2)
        {
            if (Random.Range(0,100)>50)
            {
                _newPlatform = Instantiate(platform, _spawnPos, Quaternion.identity);
            }
            else
            {
                _newPlatform = Instantiate(spikePlatform, _spawnPos, Quaternion.identity);
            }
        } if (_spawnedPlatformCounter==3)
        {
            if (Random.Range(0,100)>50)
            {
                _newPlatform = Instantiate(movingLeftPlatform, _spawnPos, Quaternion.identity);
            }
            else
            {
                _newPlatform = Instantiate(movingRightPlatform, _spawnPos, Quaternion.identity);
            }
        }

        _spawnedPlatformCounter++;
        if (_spawnedPlatformCounter==4)
        {
            _spawnedPlatformCounter = 0;
        }
    }
}
