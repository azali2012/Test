using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

//Class: Sniper Enemy Behaviour, controls the sniper enemy shooting behaviour.
///Adapted from Enemy Shooting (Tracking).
///Author: Lewis Nicoll
public class SniperEnemyBehaviour : MonoBehaviour
{
    //Public Variables:
        ///Allows you to drag in the desired bullet for the tank to fire, the point on the tank object at which the cannon pivots and a layerMask allowing
        ///the tank to effectively see through bullets to detect a player:
        public GameObject bulletObject;
        public Component cannonPivot;
        public LayerMask layerMask;

    //Private Variables:

        ///Ray Variables, this is the raycast used to detected players and the reflected path of the sniper shot:
        private Ray bulletPath;
        private RaycastHit objectHit;

        private Ray reflectionPath;
        private RaycastHit objectHitReflected;

        ///ViewDirection Variables:
        private bool isRotating;

        private Vector3 viewDirection;
        private float view_Z;
        private float view_X;
        private float rotateSpeed;

        private float deltaZ;
        private float deltaX;

        ///ReRotateVariables:
        private bool startReRotateTimer;
        private float currentRotTimer;
        private float reRotateTimer;

        ///InvertRotation variables:
        private float currentInvertRotationTimer;
        private float maxInvertRotationTimer;

        ///Sniper variables:
        private float maxSniperTime;
        private float currentSniperTime;

        private bool isSniping;

    // Start is called before the first frame update
    void Start()
    {
        //Initialising the variables:
        isRotating = true;

        view_Z = 1;
        view_X = 0;
        viewDirection = new Vector3(view_X, 0, view_Z);

        bulletPath = new Ray(transform.position, viewDirection);

        rotateSpeed = 1f;

        deltaZ = -rotateSpeed;
        deltaX = -rotateSpeed;

        startReRotateTimer = false;

        currentRotTimer = 0f;
        reRotateTimer = 1f;

        currentInvertRotationTimer = 0f;
        maxInvertRotationTimer = Random.Range(0f, 4f);

        layerMask = ~layerMask;

        maxSniperTime = 1f;
        currentSniperTime = 0f;

        isSniping = false;

    }

    // Update is called once per frame
    void Update()
    {
        //updates the tank for sniping and stops view rotation if it is:
        if (!isSniping)
        {
            ViewRotation();
        }
        if(isSniping)
        {
            currentSniperTime+= Time.deltaTime;
        }
        PlayerDetection();

        ReRotateTimer();

        InvertRotation();


        //Shows the direction the enemy tank is looking for debugging:
        Debug.DrawLine(transform.position, transform.position + viewDirection * 30f, Color.red);
    }


    //Player Detection function, checks to see if the ray cast intersects the player and snipes accordingly:
    void PlayerDetection()
    {
        //updates the origin and direction of the raycast:
        bulletPath.origin = transform.position;
        bulletPath.direction = viewDirection;
        
        //Changes the way the cannon is facing to match:
        cannonPivot.transform.rotation = Quaternion.LookRotation(bulletPath.direction, Vector3.up);

        //Reading what the bulletPath raycast hits:
        if (Physics.Raycast(bulletPath, out objectHit, 30f, layerMask))
        {
            //If the object hit is a player, the tank will attempt to snipe:
            if (objectHit.transform.tag == "Player")
            {
                if (objectHit.transform.tag == "Player")
                {
                    isSniping = true;

                    //if the player is still within the aim of the sniper after mxSniperTime the player will be destroyed:
                    if (currentSniperTime > maxSniperTime)
                    {
                        Destroy(objectHit.transform.gameObject);
                        currentSniperTime = 0;
                        isSniping = false;
                        SceneManager.LoadScene("DeathScreen");
                    }
                }
                else if (objectHit.transform.tag != "Player")
                {

                    if (currentSniperTime > maxSniperTime)
                    {
                        currentSniperTime = 0;
                        isSniping = false;
                    }
                }
            }

            //If the object hit is not the player then the reflection of the sniper tanks ray occurs and the reflected ray has a chance to snipe the player:
            if (objectHit.transform.tag != "Player")
            {
                reflectionPath = new Ray(objectHit.point, Vector3.Reflect(bulletPath.direction, objectHit.normal));
                Debug.DrawRay(objectHit.point, reflectionPath.direction * 30f, Color.red);

                if (Physics.Raycast(reflectionPath, out objectHitReflected, 30f, layerMask))
                {
                    if (objectHitReflected.transform.tag == "Player")
                    {
                        isSniping = true;
                        if (currentSniperTime > maxSniperTime)
                        {
                            Destroy(objectHitReflected.transform.gameObject);
                            currentSniperTime = 0;
                            isSniping = false;
                            SceneManager.LoadScene("DeathScreen");
                        }
                    }
                    else if (objectHitReflected.transform.tag != "Player")
                    {
                        
                        if (currentSniperTime > maxSniperTime)
                        { 
                            currentSniperTime = 0;
                            isSniping = false;
                        }
                    }
                }

            }
        }

    }


    //View Rotation function, controls the direction of the ray cast i.e. the way the enemy is looking:
    //It does this by manually pulsating two values, X and Z, to create a circle in which the view direction rotates
    void ViewRotation()
    {
        if (isRotating)
        {
            //Z:
            if (view_Z <= -1f)
            {
                view_Z = -1f;
                deltaZ = -deltaZ;
            }
            if (view_Z >= 1f)
            {
                view_Z = 1f;
                deltaZ = -deltaZ;
            }

            //X:
            if (view_X <= -1f)
            {
                view_X = -1f;
                deltaX = -deltaX;
            }
            if (view_X >= 1f)
            {
                view_X = 1f;
                deltaX = -deltaX;
            }

            view_X += deltaX * Time.deltaTime;
            view_Z += deltaZ * Time.deltaTime;


            viewDirection.z = view_Z;
            viewDirection.x = view_X;

            viewDirection = viewDirection.normalized;


        }
    }

    //The re-rotate timer function is used after the player is detected and determines how long thereafter the enemy tank should start searching again
    //e.g. if the enemy tank briefly detects the player it will remain looking in the direction of that detection for as long as the reRotateTimer
    void ReRotateTimer()
    {
        if (startReRotateTimer)
        {
            currentRotTimer += Time.deltaTime;

            if (currentRotTimer > reRotateTimer)
            {
                isRotating = true;
                currentRotTimer = 0f;
                startReRotateTimer = false;
            }
        }
    }

    //The invert roatation function switches inverts the roatation of the view direction in random intervals:
    void InvertRotation()
    {
        currentInvertRotationTimer += Time.deltaTime;

        if (currentInvertRotationTimer > maxInvertRotationTimer)
        {
            deltaZ = -deltaZ;
            deltaX = -deltaX;
            currentInvertRotationTimer = 0f;
            maxInvertRotationTimer = Random.Range(0f, 4f);
        }
    }
}
