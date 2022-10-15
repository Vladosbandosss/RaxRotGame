using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using Random = UnityEngine.Random;

public class EnemySpawner : MonoBehaviour
{
    [SerializeField] private GameObject[] asteroids;
    [SerializeField] private GameObject enemyShip;

    [SerializeField]private float _waitTimer;
    private float _minWait = 1f, _maxWait = 3f;

    private float _minXSpawnPos = -2.2f, _maxXSpawnPos = 2.2f;
    private float _XSpawnPosition;
    private Vector3 spawnPos;
    

    private void Start()
    {
        StartCoroutine(SpawnEnemies());
    }

    private IEnumerator SpawnEnemies()
    {
        _waitTimer = Random.Range(_minWait, _maxWait);

        yield return new WaitForSeconds(_waitTimer);

        _XSpawnPosition = Random.Range(_minXSpawnPos, _maxXSpawnPos);
        spawnPos = new Vector3(_XSpawnPosition, transform.position.y, transform.position.z);

        if (Random.Range(0,10)>5)
        {
            Instantiate(enemyShip, spawnPos, enemyShip.transform.rotation);
        }
        else
        {
            Instantiate(asteroids[Random.Range(0, asteroids.Length)],spawnPos, transform.rotation);
        }
        
        StartCoroutine(SpawnEnemies());
    }
}
