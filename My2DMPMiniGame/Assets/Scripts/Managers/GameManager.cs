using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class GameManager : MonoBehaviour
{
    public static GameManager Instance;

    [SerializeField]private GameObject spawnPlayerFX;

    [HideInInspector] public bool canPlayGame = false;
    [HideInInspector] public bool gameFinished = false;

    private void Awake()
    {
        if (Instance==null)
        {
            Instance = this;
        }
    }

    private void Update()
    {
        if (Input.anyKey)
        {
            canPlayGame = true;
            
            UIManager.Instance.StartGame();
        }
    }

    public void SpawnPlayerFX(Vector3 pos)
    {
        Instantiate(spawnPlayerFX,pos,Quaternion.identity);
    }
    
    
}
