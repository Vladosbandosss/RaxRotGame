using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using Random = UnityEngine.Random;

public class PlatformSpawner : MonoBehaviour
{
    public static PlatformSpawner Instance;

    [SerializeField] private GameObject leftPlatform, rightPlatform;

    private float _leftXMin = -4.4f, _leftXMax = -2.8f;
    private float _rightXMin = 4.4f, _rightXMax = 2.8f;

    private float _yTreshold = 2.6f;
    private float _lastY;

    public int spawnCount = 8;
    private int _platFormSpawned;

    [SerializeField] private Transform platformParent;

    private void Awake()
    {
        if (Instance==null)
        {
            Instance = this;
        }
    }

    private void Start()
    {
        _lastY = transform.position.y;
        
        SpawnPlatforms();
    }

    public void SpawnPlatforms()
    {
        Vector2 temp = transform.position;
        GameObject newPlatform = null;

        for (int i = 0; i < spawnCount; i++)
        {
            temp.y = _lastY;
            if (_platFormSpawned%2==0)
            {
                temp.x = Random.Range(_leftXMin, _leftXMax);
                newPlatform = Instantiate(rightPlatform, temp, Quaternion.identity);
            }
            else
            {
                temp.x = Random.Range(_rightXMin, _rightXMax);
                newPlatform = Instantiate(leftPlatform, temp, Quaternion.identity);
            }

            newPlatform.transform.parent = platformParent;

            _lastY += _yTreshold;
            _platFormSpawned++;
        }
    }
}
