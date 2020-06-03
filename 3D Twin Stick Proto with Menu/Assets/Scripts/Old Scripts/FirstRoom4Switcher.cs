using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class FirstRoom4Switcher : MonoBehaviour
{
    // Start is called before the first frame update
    bool roomOneActive;
    bool roomFourActive;
    public Camera roomOneCam;
    public Camera roomFourCam;
    void Start()
    {
        roomFourCam.enabled = false;
        roomOneActive = true;
        roomFourActive = false;
    }

  
 
    void OnTriggerEnter(Collider other)
    {
        if (other.tag == "Player" && roomOneActive == true)
        {
            roomOneCam.enabled = false;
            roomFourCam.enabled = true;
            roomOneActive = false;

        }

        if (other.tag == "Player" && roomFourActive == true)
        {
            roomOneCam.enabled = true;
            roomFourCam.enabled = false;
            roomOneActive = true;


        }
    }
    void OnTriggerExit(Collider other)
    {
        if (roomOneActive == false)
        {
            roomFourActive = true;
        }
        if (roomFourActive == false)
        {
            roomOneActive = true;
        }
        if (roomOneActive == true)
        {
            roomFourActive = false;
        }

    }
}