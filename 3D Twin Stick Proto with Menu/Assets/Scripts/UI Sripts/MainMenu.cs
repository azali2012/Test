using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

//This Script has been done by Aziz Ali
public class MainMenu : MonoBehaviour
{
    //This quits the application
    public void QuitGame()
    {
        Debug.Log("Quit!!!");
        Application.Quit();
    }

    //This loads the Arena scene
    public void loadArena()
    {
        SceneManager.LoadScene("MerlinsLevelTemplate");
    }

    //This loads the Classic scene
    public void LoadClassic()
    {
        SceneManager.LoadScene("ClassicLevel1");
    }

    //This loads the PvP scene
    public void LoadPvP()
    {
        SceneManager.LoadScene("");
    }
}