using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Bomb : MonoBehaviour
{
    public static Bomb Instance;
    
    private PlayerController _player;

    [SerializeField] private float explodeDelay = 3f;
    //private float _explosionTimer = 0;

    [SerializeField] private GameObject explosionPrefab;
    [SerializeField] private Transform pointToExplode;
    [SerializeField] private float explodeSpeed = 4f;
    [SerializeField] private float explodeRange;

    private void Awake()
    {
        if (Instance==this)
        {
            Instance = this;
        }
    }

    private void Start()
    {
        _player = GameObject.FindWithTag(TagManager.PLAYER_TAG).GetComponent<PlayerController>();
        
        Invoke(nameof(Explode),explodeDelay);

        explodeRange = GameManager.Instance.bombRange;
        
        UIManager.Instance.UpdateRange(explodeRange);
    }
    
    public void Explode()
    {
        AudioManager.Instance.BombExplodedSFX();
        
        GameObject explosionUp = Instantiate(explosionPrefab, pointToExplode.position, Quaternion.identity);
        explosionUp.GetComponent<Explosion>().SetMoveVector(Vector3.forward,explodeSpeed,explodeRange);
        
        GameObject explosionDown = Instantiate(explosionPrefab, pointToExplode.position, Quaternion.identity);
        explosionDown.GetComponent<Explosion>().SetMoveVector(Vector3.back,explodeSpeed,explodeRange);
        
        GameObject explosionLeft = Instantiate(explosionPrefab, pointToExplode.position, Quaternion.identity);
        explosionLeft.GetComponent<Explosion>().SetMoveVector(Vector3.left,explodeSpeed,explodeRange);
        
        GameObject explosionRight = Instantiate(explosionPrefab, pointToExplode.position, Quaternion.identity);
        explosionRight.GetComponent<Explosion>().SetMoveVector(Vector3.right,explodeSpeed,explodeRange);
        
        FinishExplode();
        
        _player.BombExploded();
    }

    private void FinishExplode()
    {
        Destroy(gameObject);
    }

    private void OnTriggerExit(Collider other)
    {
        if (other.CompareTag(TagManager.PLAYER_TAG))
        {
            GetComponent<SphereCollider>().isTrigger = false;
        }
    }
}
