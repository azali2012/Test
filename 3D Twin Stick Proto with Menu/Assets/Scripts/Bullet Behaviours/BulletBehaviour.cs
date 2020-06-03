using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class BulletBehaviour : MonoBehaviour
{
    //Private Variables:
    private Rigidbody m_rigidbody;

    private int     maxBounces;
    private int     moveTimes  = 3;
    private int     currentBounces;

    // Start is called before the first frame update
    void Start()
    {
        //Initialising Variables:
        m_rigidbody = GetComponent<Rigidbody>();
      
    }

    // Update is called once per frame
    void Update()
    {
       Movement();
       
    }

   

    //Bullet Movement:
    void Movement()
    { 

        while (moveTimes >=0 )
        {
            m_rigidbody.AddForce(transform.forward * 10.0f);
            moveTimes -= 1;
        }
          
      
    }
}
