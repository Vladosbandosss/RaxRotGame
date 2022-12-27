using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using Random = UnityEngine.Random;

public class ArenaManager : MonoBehaviour
{
    public static ArenaManager Instance;
    
    [SerializeField] private Transform[] healSpawnPos;
    [SerializeField] private GameObject healHeart;
    private GameObject _spawnedHeart;

    private int _indexToSpawn;

    [SerializeField] private float minSpawnTime = 15f, maxSpawnTime = 25f;
    [SerializeField] private float minDestroyTime = 10f, maxDestroyTime = 15f;

    private void Awake()
    {
        if (Instance==null)
        {
            Instance = this;
        }
    }

    private void Start()
    {
        StartSpawn();
    }

    private IEnumerator _SpawnHearT()
    {
        yield return new WaitForSeconds(Random.Range(minSpawnTime, maxSpawnTime));
        _indexToSpawn = Random.Range(0, healSpawnPos.Length);

        _spawnedHeart = Instantiate(healHeart, healSpawnPos[_indexToSpawn].position, Quaternion.identity);

        yield return new WaitForSeconds(Random.Range(minDestroyTime, maxDestroyTime));
        
        Destroy(_spawnedHeart);
        
        StartCoroutine(nameof(_SpawnHearT));
    }

    public void StartSpawn()
    {
        StartCoroutine(nameof(_SpawnHearT));
    }
}
