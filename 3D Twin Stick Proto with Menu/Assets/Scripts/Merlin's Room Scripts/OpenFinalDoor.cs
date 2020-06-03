using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class OpenFinalDoor : MonoBehaviour
{
    //picks up the key and activates it 
    void OnTriggerEnter(Collider other)
    {
        GameMode.finalKeyActive = true;
        GameMode.keySpawnable = false;
        Destroy(gameObject);
    }

}
