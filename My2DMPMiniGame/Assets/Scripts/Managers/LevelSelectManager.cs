using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class LevelSelectManager : MonoBehaviour
{
    public void PlayArena1()
    {
        SceneManager.LoadScene(TagManager.ARENA1_LEVEL_NAME);
    }
    
    public void PlayArena2()
    {
        SceneManager.LoadScene(TagManager.ARENA2_LEVEL_NAME);
    }
    
    public void PlayArena3()
    {
        SceneManager.LoadScene(TagManager.ARENA3_LEVEL_NAME);
    }
    
    public void PlayArena4()
    {
        SceneManager.LoadScene(TagManager.ARENA4_LEVEL_NAME);
    }
}
