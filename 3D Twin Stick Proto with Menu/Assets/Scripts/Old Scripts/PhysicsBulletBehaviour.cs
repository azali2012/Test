using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PhysicsBulletBehaviour : MonoBehaviour
{
    //Private Variables:
    private float moveSpeed;

    private Rigidbody rb;

    // Start is called before the first frame update
    void Start()
    {
        moveSpeed = 20;

        rb = GetComponent<Rigidbody>();
    }

    // Update is called once per frame
    void Update()
    {
        //rb.rotation = transform.rotation;
        rb.velocity = new Vector3( 0,0,moveSpeed * Time.deltaTime);
    }
}
