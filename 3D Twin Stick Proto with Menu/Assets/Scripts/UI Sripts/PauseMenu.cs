using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

//This scripts has been done by Aziz Ali
public class PauseMenu : MonoBehaviour
{
    //Variables
    public static bool GamePaused = false;
    //GameObjects
    public GameObject PauseMenuUI;

    // Update is called once per frame
    void Update()
    {
        //This allows you to press spacebar to pause and resume the game
        if (Input.GetKeyDown(KeyCode.Space))
        {
            if(GamePaused)
            {
                Resume();
            }else
            {
                Pause();
            }
        }
    }

    //This function resumes the game once paused
    public void Resume ()
    {
        PauseMenuUI.SetActive(false);
        //This returns everything in the scene back to normal speed
        Time.timeScale = 1f;
        GamePaused = false;
    }

    //This function pauses the game once the spacebar key has been pressed
    void Pause ()
    {
        PauseMenuUI.SetActive(true);
        //This stops everything within the scene
        Time.timeScale = 0f;
        GamePaused = true;
    }

    //This loads the main menu scene
    public void LoadMainMenu ()
    {
        Debug.Log("Loading Menu");
        SceneManager.LoadScene("MainMenu");
        Resume();
    }

    //This quits the application
    public void QuitGame ()
    {
        Debug.Log("Quit!!!!!");
        Application.Quit();
    }
}