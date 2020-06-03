using System.Collections;
using System.Collections.Generic;
using UnityEngine;

//Class: Enemy Shooting (Tracking), controls the enemy shooting behaviour and tracks the player when it is detected.
///Several other enemy shooting styles are adapted from this script.
///Author: Lewis Nicoll
public class EnemyShootingTracking : MonoBehaviour
{
    //Public Variables:

        ///Allows you to drag in the desired bullet for the tank to fire, the point on the tank object at which the cannon pivots and a layerMask allowing
        ///the tank to effectively see through bullets to detect a player:
        public GameObject bulletObject;
        public Component cannonPivot;
        public LayerMask layerMask;

    //Private Variables:

        ///Ray Variables, this is the raycast used to detected players:
        private Ray bulletPath;
        private RaycastHit objectHit;

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

        ///PlayerDetectionVariables:
        private float timeBetweenShots;
        private float timePassed;

        ///Tracking variables:
        private bool isTracking;
        private Vector3 playerPosition;

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

        isTracking = false;

        layerMask = ~layerMask;

        timeBetweenShots = 1f;
        timePassed = 0f;

    }

    // Update is called once per frame
    void Update()
    {
        ViewRotation();

        PlayerDetection();

        ReRotateTimer();

        InvertRotation();

        //Shows the direction the enemy tank is looking for debugging:
        Debug.DrawLine(transform.position, transform.position + viewDirection * 30f);
    }


    //Player Detection function, checks to see if the ray cast intersects the player and fires a bullet accordingly:
    void PlayerDetection()
    {
        //updates the origin of the raycast to the tanks position:
        bulletPath.origin = transform.position;

        //tank effectively has two modes: tracking and not tracking, if the tank is not tracking it's detection ray will be drawn parallel to it's view direction, 
        //if it is tracking it's detection ray will point in the direction of the the player
        if (!isTracking)
        {
            bulletPath.direction = viewDirection;
        }
        if (isTracking)
        {
            bulletPath.direction = playerPosition - transform.position;
        }

        //Changes the way the cannon is facing to match:
        cannonPivot.transform.rotation = Quaternion.LookRotation(bulletPath.direction, Vector3.up);

        //Reading what the bulletPath raycast hits:
        if (Physics.Raycast(bulletPath, out objectHit, 30f, layerMask))
        {
            //If the object hit is a player, the tank will attempt to shoot and it will begin tracking the player:
            if (objectHit.transform.tag == "Player")
            {
                if (timePassed > timeBetweenShots)
                {
                    Instantiate(bulletObject, transform.position + 1.5f * bulletPath.direction, Quaternion.LookRotation(bulletPath.direction, Vector3.up));
                    timePassed = 0f;
                }
                isTracking = true;
                playerPosition = objectHit.transform.position;
            }
            //If the object hit is not the player then the tank does not track the player:
            if (objectHit.transform.tag != "Player")
            {
                isTracking = false;
            }
        }

        //Updates the Time passed:
        timePassed += Time.deltaTime;
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
