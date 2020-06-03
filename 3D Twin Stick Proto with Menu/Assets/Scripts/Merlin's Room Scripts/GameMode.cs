using System.Collections;
using System.Collections.Generic;
using UnityEngine;


//This script holds all of the global variables to be used across the gamemode
public class GameMode: MonoBehaviour
{

    public static int enemiesLeft;
    public GameObject doorOne;
    //public GameObject doorTwo;
    public GameObject finalDoorOne;
    public GameObject finalDoorTwo;
    public GameObject key;
    public Camera roomThreeCam;
    public static bool roomOneActive;
    public static bool roomTwoActive;
    public static bool roomThreeActive;
    public static bool roomFourActive;
    public static bool finalKeyActive;
    public static bool keySpawnable;

    // Start is called before the first frame update
    void Start()
    {
        enemiesLeft = 2;
        roomTwoActive = false;
        roomThreeActive = false;
        roomFourActive = false;
        finalKeyActive = false;
        keySpawnable = false;
        roomThreeCam.enabled = false;
  
     
    }

    // Update is called once per frame
    void Update()
    {
        //code for spawning the key in the last room and also making sure that the correct doors are spawned/despawned

        if (keySpawnable == true && enemiesLeft <=0)
        {
            key.gameObject.SetActive(true);
        }

        if (enemiesLeft <= 0)
        {
            doorOne.gameObject.SetActive(false);
            //doorTwo.gameObject.SetActive(false);
        }
        else if (enemiesLeft > 0)
        {
            doorOne.gameObject.SetActive(true);
            //doorTwo.gameObject.SetActive(true);
            finalDoorOne.gameObject.SetActive(true);
            finalDoorTwo.gameObject.SetActive(true);
        }
      
   
    }
}
