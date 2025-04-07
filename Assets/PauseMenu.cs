using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class PauseMenu : MonoBehaviour
{

   public GameObject PauseMenuGO;

   public void Quit()
   {
      {
#if UNITY_EDITOR
         UnityEditor.EditorApplication.isPlaying = false;
#else
        Application.Quit();
#endif
      }
   }

   public void RestartGame()
   {
      Time.timeScale = 1;
      SceneManager.LoadScene(SceneManager.GetActiveScene().buildIndex);
   }

   private void Update()
   {
      if (Input.GetKeyDown(KeyCode.Escape))
      {
         if(Time.timeScale == 0)
         {
            UnPause();
         }
         else
         {
            Pause();
         }
      }
   }

   public void Pause()
   {
      Time.timeScale = 0;
      PauseMenuGO.SetActive(true);
   }

   public void UnPause()
   {
      Time.timeScale = 1;
      PauseMenuGO.SetActive(false);
   }
   public void RestartLevel()
   {
      UnPause();
      SceneManager.LoadScene(SceneManager.GetActiveScene().buildIndex);
   }
}
