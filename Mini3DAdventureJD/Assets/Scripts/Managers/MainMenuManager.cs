using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class MainMenuManager : MonoBehaviour
{
   public void NewGame()
   {
      PlayerPrefs.DeleteAll();
      
   }

   public void ContinueGame()
   {
      
   }

   public void QuitGame()
   {
      Application.Quit();
   }
}
