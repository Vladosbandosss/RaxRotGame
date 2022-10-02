using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class LevelExit : MonoBehaviour
{
    [SerializeField] private string levelToLoad;

    [SerializeField] private float waitForNextLevelLoading;

    private void OnTriggerEnter(Collider other)
    {
        if (other.CompareTag(TagManager.PLAYER_TAG))
        {
            EndLevel();
        }
    }

    private IEnumerator EndLevelCO()
    {
        GameManager.Instance.loadingNextLevel = true;
        
        yield return new WaitForSeconds(waitForNextLevelLoading);

        SceneManager.LoadScene(levelToLoad);
    }

    private void EndLevel()
    {
        StartCoroutine(EndLevelCO());
    }
}
