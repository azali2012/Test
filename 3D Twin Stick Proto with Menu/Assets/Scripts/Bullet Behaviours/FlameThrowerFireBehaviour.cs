using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

//Class: Flamethrower Fire Behaviour, controls the behaviour of the fire particles used by the flamethrower.

///Author: Lewis Nicoll
public class FlameThrowerFireBehaviour : MonoBehaviour
{
    //Private Variables:
    private Vector3 maxScale;
    private float moveSpeed;

    // Start is called before the first frame update
    void Start()
    {
        //Initialising variables:
        maxScale = new Vector3(1f,1f,1f);
        moveSpeed = 10f;
    }

    // Update is called once per frame
    void Update()
    {
        Movement();
    }

    //Collisions, destroying the player and bullet on impact:
    void OnCollisionEnter( Collision collision )
    {
        if(collision.transform.tag == "Player")
        {
            Destroy(collision.gameObject);
            SceneManager.LoadScene("DeathScreen");
        }
        if (collision.transform.tag == "Bullet")
        {
            Destroy(collision.gameObject);
           
        }


    }

    //Movement function, controls the movement of the fire particles:
    void Movement()
    {
        //Lerping the fire particles scale and speed to create the flamethrower effect:
        transform.localScale = Vector3.Lerp(transform.localScale, maxScale, 0.5f * Time.deltaTime);
        transform.Translate(Vector3.forward * moveSpeed * Time.deltaTime);
        moveSpeed = Mathf.Lerp(moveSpeed, 0f, 0.99f * Time.deltaTime);

        //If the speed is below the threshold the fire particle is destroyed:
        if (moveSpeed < 1f)
        {
            Destroy(gameObject);
        }
    }
}
