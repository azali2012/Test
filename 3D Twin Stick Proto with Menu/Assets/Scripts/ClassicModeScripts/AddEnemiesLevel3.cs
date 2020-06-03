using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class AddEnemiesLevel3 : MonoBehaviour
{
   void OnTriggerExit(Collider other)
    {
        GameMode.enemiesLeft += 1;
        Destroy(gameObject);
    }
    
}
