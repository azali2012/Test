using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ActivateFinalRoom : MonoBehaviour
{

    public GameObject finalDoor;
    public GameObject enemyOne;
    public GameObject enemyTwo;
    public GameObject enemyThree;


    // Start is called before the first frame update
    void Start()
    {
        //setting all the enemies to be inactive so you have to activate the room to make them active 
        enemyOne.SetActive(false);
        enemyTwo.SetActive(false);
        enemyThree.SetActive(false);
    }

    // Update is called once per frame
    void Update()
    { 

    }

    void OnTriggerExit(Collider other)
    {
        //sets up the room when you exit the collider by the door by activating the enemies and updating the enemies left 
        GameMode.enemiesLeft += 3;
        finalDoor.gameObject.SetActive(true);
        enemyOne.SetActive(true);
        enemyTwo.SetActive(true);
        enemyThree.SetActive(true);
        Destroy(gameObject);
    }

}
