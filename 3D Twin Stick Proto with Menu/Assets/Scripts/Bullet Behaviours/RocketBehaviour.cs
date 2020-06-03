using System.Collections;
using System.Collections.Generic;
using UnityEngine;

//Class: Rocket Behaviour, controls the behaviour of rockets in the game.
///Description: Is effectively just a faster ray trace bullet and is adapted from the script.
///Author: Lewis Nicoll
public class RocketBehaviour : MonoBehaviour
{
    //Public Variables:


    //Private Variables:

        ///Move speed variable:
        private float moveSpeed;

        ///Variables for the bullet's path and reflections:
        private Ray bulletPath;
        private RaycastHit objectHit;
        private Vector3 reflectDirection;
        private float newRotation;

        ///Bounce variables:
        private int currentBounces;
        private int maxBounces;

    // Start is called before the first frame update
    void Start()
    {
        //Initialising variables:
        moveSpeed = 10;

        bulletPath = new Ray(transform.position, transform.forward);

        currentBounces = 0;
        maxBounces = 1;
    }

    // Update is called once per frame
    void Update()
    {
        Movement();
        Reflections();
    }

    //Bullet Movement:
    void Movement()
    {
        transform.Translate(Vector3.forward * moveSpeed * Time.deltaTime);
    }

    //The reflections functions, dictates what the bullet should do when it's path is about to collide with another object
    //including reflecting if has enough bounces remaining:
    void Reflections()
    {
        //updating the ray:
        bulletPath.origin = transform.position;
        bulletPath.direction = transform.forward;

        //Reading what the ray has collided with:
        if (Physics.Raycast(bulletPath, out objectHit, moveSpeed * Time.deltaTime + .1f))
        {
            if (objectHit.collider.gameObject != gameObject)
            {
                //Destroying the bullet and the object hit if it is a tank or another bullet:
                if (objectHit.transform.tag == "Bullet")
                {
                    Destroy(objectHit.collider.gameObject);
                    Destroy(gameObject);
                }
                else if (objectHit.transform.tag == "Tank")
                {
                    Destroy(objectHit.collider.transform.parent.gameObject);
                    Destroy(gameObject);
                    GameMode.enemiesLeft -= 1;
                }
                else if (objectHit.transform.tag == "Player")
                {
                    Destroy(objectHit.collider.transform.parent.gameObject);
                    Destroy(gameObject);
                }

                //If the bullet hit's something else, it will reflect, assuming it has enough bounces remaining:
                else if (currentBounces < maxBounces)
                {
                    //Calculating the new reflected direction using the normal of the surface the bullet's ray hit:
                    reflectDirection = Vector3.Reflect(bulletPath.direction, objectHit.normal);
                    newRotation = 90f - Mathf.Atan2(reflectDirection.z, reflectDirection.x) * Mathf.Rad2Deg;
                    transform.position = objectHit.point;
                    transform.eulerAngles = new Vector3(0, newRotation, 0);
                    currentBounces++;
                }

                //Destroying the bullet if it has no remaining bounces:
                else if (currentBounces >= maxBounces)
                {
                    Destroy(gameObject);
                }
            }
        }
    }
}
