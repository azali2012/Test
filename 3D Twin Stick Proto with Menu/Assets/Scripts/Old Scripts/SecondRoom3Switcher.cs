using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class SecondRoom3Switcher : MonoBehaviour
{
    // Start is called before the first frame update
    bool roomTwoActive;
    public static bool roomThreeActive;
    public Camera roomTwoCam;
    public Camera roomThreeCam;
    void Start()
    {
        roomThreeCam.enabled = false;
        roomTwoActive = true;
        roomThreeActive = false;
    }

    // Update is called once per frame
    void Update()
    {

    }

    void OnTriggerEnter(Collider other)
    {
        if (other.tag == "Player" && roomTwoActive == true)
        {
            roomTwoCam.enabled = false;
            roomThreeCam.enabled = true;
            roomTwoActive = false;

        }

        if (other.tag == "Player" && roomThreeActive == true)
        {
            roomTwoCam.enabled = true;
            roomThreeCam.enabled = false;
            roomTwoActive = true;


        }
    }
    void OnTriggerExit(Collider other)
    {
        if (roomTwoActive == false)
        {
            roomThreeActive = true;
        }
        if (roomThreeActive == false)
        {
            roomTwoActive = true;
        }
        if (roomTwoActive == true)
        {
            roomThreeActive = false;
        }

    }
}
