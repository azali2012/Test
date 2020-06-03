using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class RoomOnetoTwoSwitcher : MonoBehaviour
{

    public GameObject playerOneTank;
    public Camera roomOneCam;
    public Camera roomTwoCam; 

    void start()
    {
        //Makes sure the correct camera is active when the scene is loaded
        roomTwoCam.enabled = false;
    }

    void OnTriggerEnter()
  
    {
        //activates the next room when the player walks into the trigger
        GameMode.roomOneActive = false;
        GameMode.roomTwoActive = true;
        roomOneCam.enabled = false;
        roomTwoCam.enabled = true;
        playerOneTank.transform.position += new Vector3(0, 0, 20); 
    }

}

