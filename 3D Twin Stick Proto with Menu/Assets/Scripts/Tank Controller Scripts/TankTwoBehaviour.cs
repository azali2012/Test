using System.Collections;
using System.Collections.Generic;
using UnityEngine;


public class TankTwoBehaviour : MonoBehaviour
{

    public float tankTwoHealth;

   
    // Start is called before the first frame update
    void Start()
    {
        tankTwoHealth = 100;
    }

    // Update is called once per frame
    void Update()
    {
        ClampHealth();
        KillTankTwo();
    }

    void DamageTankTwo()
    {
        tankTwoHealth -= 0.3f;
    }

    void OnTriggerStay(Collider other)
    {
        if (other.gameObject.tag == "Death Cube")
        {
            DamageTankTwo();
        }
    }

    void OnTriggerEnter(Collider other)
    {
        if (other.gameObject.tag == "Health Pickup")
        {
            tankTwoHealth += 50;
            Destroy(other.gameObject);
        }
    }

    void KillTankTwo()
    {
        if (tankTwoHealth <= 0)
            Destroy(gameObject);
    }

    void ClampHealth()
    {
        if (tankTwoHealth >= 100)
            tankTwoHealth = 100;
    }

}
