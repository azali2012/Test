using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class WaterBullet : MonoBehaviour
{
    // Start is called before the first frame update
    void Start()
    {

    }

    // Update is called once per frame
    void Update()
    {

    }
    void OnCollisionEnter( Collision collision )
    {
        if (collision.gameObject.tag == "Water")
        {
            Destroy(collision.gameObject);
        }
    }
}
