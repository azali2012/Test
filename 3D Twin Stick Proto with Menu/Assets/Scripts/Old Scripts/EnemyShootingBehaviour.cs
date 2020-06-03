using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class EnemyShootingBehaviour : MonoBehaviour
{
    //Public Variables:
        public GameObject bulletObject;
        public Component cannonPivot;

    //Private Variables:

        //Ray Variables:
        private Ray bulletPath;
        private RaycastHit objectHit;

        //ViewDirection Variables:
        private bool isRotating;

        private Vector3 viewDirection;
        private float view_Z;
        private float view_X;
        private float rotateSpeed;

        private float deltaZ;
        private float deltaX;

        //ReRotateVariables:
        private bool startReRotateTimer;
        private float currentRotTimer;
        private float reRotateTimer;

        //InvertRotation variables:
        private float currentInvertRotationTimer;
        private float maxInvertRotationTimer;    

    // Start is called before the first frame update
    void Start()
    {
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
        reRotateTimer   = 1f;

        currentInvertRotationTimer = 0f;
        maxInvertRotationTimer = Random.Range(0f,4f);

    }

    // Update is called once per frame
    void Update()
    {
        ViewRotation();
        
        PlayerDetection();

        ReRotateTimer();

        InvertRotation();
        
        Debug.DrawLine(transform.position, transform.position + viewDirection* 30f);
    }


    //Player Detection function, checks to see if the ray cast intersects the player and fires a bullet accordingly:
    void PlayerDetection()
    {
        bulletPath.direction = viewDirection;

        if (Physics.Raycast(bulletPath , out objectHit, 30f))
        {
            
            if (objectHit.transform.tag == "Player")
            {
                Instantiate(bulletObject, transform.position + 1.5f * viewDirection, Quaternion.LookRotation(viewDirection, Vector3.up));
                isRotating = false;
                startReRotateTimer = true;
            }
        }
    }


    //View Rotation function, controls the direction of the ray cast i.e. the way the enemy is looking:
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

            //Changes the way the cannon is facing to match:
            cannonPivot.transform.rotation = Quaternion.LookRotation(viewDirection, Vector3.up);
        }
    }

    void ReRotateTimer()
    {
        if(startReRotateTimer)
        {
            currentRotTimer += Time.deltaTime;

            if(currentRotTimer > reRotateTimer)
            {
                isRotating = true;
                currentRotTimer = 0f;
                startReRotateTimer = false;
            }
        }
    }

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
