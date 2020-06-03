using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class RoomTwotoOnwSwitcher : MonoBehaviour
{

    public GameObject playerOneTank;
    public Camera roomOneCam;
    public Camera roomTwoCam; 

    void start()
    {
        //makes sure the correct camera is active when the scene is loaded
        roomTwoCam.enabled = true;
    }

    void OnTriggerEnter()
    {
        //activates the next room when the player walks into the trigger
        GameMode.roomOneActive = true;
        GameMode.roomTwoActive = false;
        roomOneCam.enabled = true;
        roomTwoCam.enabled = false;
        playerOneTank.transform.position -= new Vector3(0, 0, 20); 
    }

}

