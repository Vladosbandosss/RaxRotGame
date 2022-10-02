using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class EnemyHealth : MonoBehaviour
{
    [SerializeField] private int currentHealth = 5;

    [SerializeField] private GameObject enemyDeadFX;

    public void DamageEnemy(int damage)
    {
        currentHealth-=damage;
        
        //AudioManager.Instance.PlaySFX();

        if (currentHealth<=0)
        {
            Instantiate(enemyDeadFX, transform.position, Quaternion.identity);
            
            AudioManager.Instance.PlaySFX(14);
            
            Destroy(gameObject);
        }
    }
}
