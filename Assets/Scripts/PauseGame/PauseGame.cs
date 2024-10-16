using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PauseGame : MonoBehaviour
{
   public void StartPauseGame()
   {
        Time.timeScale = 0.0f;
   }

   public void UnpauseGame()
   {
        Time.timeScale = 1.0f;
   }
}
