using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class UnlockFinalDoor : MonoBehaviour
{

    public GameObject finalDoorOne;
    public GameObject finalDoorTwo;
    // Start is called before the first frame update
    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {
        
    }

    //opens the final door if the player walks over the trigger box while holding the key 
    void OnTriggerEnter(Collider other)
    {
         if (GameMode.finalKeyActive == true)
            finalDoorOne.gameObject.SetActive(false);
             finalDoorTwo.gameObject.SetActive(false);
  
    }
}
