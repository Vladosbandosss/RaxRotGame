using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class CheckPoint : MonoBehaviour
{
    [SerializeField] private string cpName;
    private string _cpName = "_cp";

    private void Start()
    {
        if (PlayerPrefs.HasKey(SceneManager.GetActiveScene().name+ _cpName))
        {
            if (PlayerPrefs.GetString(SceneManager.GetActiveScene().name + _cpName)==cpName)
            {
                PlayerController.Instance.transform.position = transform.position;
            }
        }
    }

    private void OnTriggerEnter(Collider other)
    {
        if (other.CompareTag(TagManager.PLAYER_TAG))
        {
            PlayerPrefs.SetString(SceneManager.GetActiveScene().name + _cpName,cpName);
            
            AudioManager.Instance.PlaySFX(1);
        }
    }
}
