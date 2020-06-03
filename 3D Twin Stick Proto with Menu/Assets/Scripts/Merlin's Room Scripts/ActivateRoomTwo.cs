using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ActivateRoomTwo : MonoBehaviour
{

    public GameObject doorTwo;
    public GameObject enemyOne;
    public GameObject enemyTwo;
   

    // Start is called before the first frame update
    void Start()
    {
        //setting all the enemies to be inactive so you have to activate the room to make them active
        enemyOne.SetActive(false);
        enemyTwo.SetActive(false);
  
    }

    // Update is called once per frame
    void Update()
    {
        //if(GameMode.enemiesLeft <=0)
        //{
        //    doorTwo.gameObject.SetActive(false);
        //}
    }

    void OnTriggerExit(Collider other)
    {
        //sets up the room when you exit the collider by the door by activating the enemies and updating the enemies left and makes the key spawnable
        GameMode.enemiesLeft += 2;
      //  doorTwo.SetActive(true);
        enemyOne.SetActive(true);
        enemyTwo.SetActive(true);
        GameMode.keySpawnable = true; 
        Destroy(gameObject);
    }

}
