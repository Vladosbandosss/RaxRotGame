using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CheckPointController : MonoBehaviour
{
    public static CheckPointController instance;

     private CheckPoint[] checkPoints;

     public Vector3 spawnPoint;

    private void Awake()
    {
        if (instance==null)
        {
            instance = this;
        }

        //spawnPoint = PlayerController.instnace.transform.position;
    }

    private void Start()
    {
        checkPoints = FindObjectsOfType<CheckPoint>();
    }

    public void DeactivateCheckPoints()
    {
        for (int i = 0; i < checkPoints.Length; i++)
        {
            checkPoints[i].ResetCheckPoint();
        }
    }

    public void SetSpawnPoint(Vector3 newSpawnPoint)
    {
        spawnPoint = newSpawnPoint;
    }
}
