using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class RoomOneToFinalSwitcher : MonoBehaviour
{

    public GameObject playerOneTank;
    public Camera roomOneCam;
    public Camera roomThreeCam;

    void start()
    {
        //makes sure the correct camera is active when the scene is loaded
        roomThreeCam.enabled = false;
    }

    void OnTriggerEnter()
    {
        //activates the next room when the player walks into the trigger
        GameMode.roomOneActive = false;
        GameMode.roomThreeActive = true;
        roomOneCam.enabled = false;
        roomThreeCam.enabled = true;
        playerOneTank.transform.position -= new Vector3(0, 0, 20);
    }

}


