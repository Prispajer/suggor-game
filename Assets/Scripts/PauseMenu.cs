using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class PauseMenu : MonoBehaviour
{
    public static bool gameIsPaused = false;

    
    
    public GameObject PauseMenuUI;

    void Update()
    {
        if (!gameIsPaused)
        {
            Cursor.visible = false;
            Cursor.lockState = CursorLockMode.Locked;

        }
        else if (gameIsPaused)
        {
            Cursor.visible = true;
            Cursor.lockState = CursorLockMode.None;

        }
        if (Input.GetKeyDown(KeyCode.Escape))
        {

            if (gameIsPaused)
            {
                Resume();

            }
            else
            {
                Pause();
  

            }
        }
     
    }

    public void Resume()
    {
        PauseMenuUI.SetActive(false);
        Time.timeScale = 1f;
        gameIsPaused = false;
        
        
    }

     public void Pause()
    {
  
        PauseMenuUI.SetActive(true);
        Time.timeScale = 0f;
        gameIsPaused = true;

    }

    public void Exit()
    {
        Application.Quit();
    }

    public void MainMenu()
    {
        SceneManager.LoadScene(0);
        Resume();

    }


}
