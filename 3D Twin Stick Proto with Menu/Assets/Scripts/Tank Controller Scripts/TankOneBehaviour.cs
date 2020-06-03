using System.Collections;
using System.Collections.Generic;
using UnityEngine;


public class TankOneBehaviour : MonoBehaviour
{

    public float tankOneHealth;

   
    // Start is called before the first frame update
    void Start()
    {
        tankOneHealth = 100;
    }

    // Update is called once per frame
    void Update()
    {
        ClampHealth();
        KillTankOne();
    }

    void DamageTankOne()
    {
        tankOneHealth -= 0.3f;
    }

    void OnTriggerStay(Collider other)
    {
        if (other.gameObject.tag == "Death Cube")
        {
            DamageTankOne();
        }
    }

    void OnTriggerEnter(Collider other)
    {
        if (other.gameObject.tag == "Health Pickup")
        {
            tankOneHealth += 50;
            Destroy(other.gameObject);
        }
    }

    void KillTankOne()
    {
        if (tankOneHealth <= 0)
            Destroy(gameObject);
    }

    void ClampHealth()
    {
        if (tankOneHealth >= 100)
            tankOneHealth = 100;
    }

}
