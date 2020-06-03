using System.Collections;
using System.Collections.Generic;
using UnityEngine;

//Class: Enemy Movement, controls the enemy movement behaviour.
///Description: Causes the enemy tanks to move in a radom direction for a random length of time, if the tank detects a wall
/// the tanks flips its direction, this currently however can only happen once per random direction and needs updating.
///Author: Lewis Nicoll, Merlin Aldrick
public class EnemyMovement : MonoBehaviour
{
    //Private Variables:
        private float moveSpeed;
        private float moveTimer;
        private float maxDirectionTime;
        private Vector3 moveDirection;
        private Quaternion targetRotation;
        public float maxRotationSpeed = 200.0f;
        public Transform tankBody;

        // Ray variables for wall detection:
        private Ray movementPath;
        private RaycastHit objectHit;

        private bool isReversing;

    // Start is called before the first frame update
    void Start()
    {
        //Initialising variables:
        moveSpeed = 1f;
        moveTimer = 0f;
        maxDirectionTime = 7.5f;
        moveDirection = new Vector3(Random.Range(-1f, 1f), 0f, Random.Range(-1f, 1f));
        moveDirection = moveDirection.normalized;

        movementPath = new Ray(transform.position, moveDirection);

        isReversing = false;
    }

    // Update is called once per frame
    void Update()
    {
        Movement();

        WallDetection();

    }

    //Movement controls the basic random movement of the enemy tank:
    void Movement()
    {
        //Checks to see if the tank is reversing or not, changes direction accordingly:
        if (!isReversing)
        {
            transform.position = transform.position + moveDirection * moveSpeed * Time.deltaTime;
        }
        if (isReversing)
        {
            transform.position = transform.position - moveDirection * moveSpeed * Time.deltaTime;
        }
        //Updating the move timer:
        moveTimer += Time.deltaTime;

        //If the move timer exceeds the max direction time, a new random direction with a random duration is generated:
        if (moveTimer > maxDirectionTime)
        {
            moveTimer = 0f;
            moveDirection = new Vector3(Random.Range(-1f, 1f), 0f, Random.Range(-1f, 1f));
            moveDirection = moveDirection.normalized;
            isReversing = false;
        }

         //*   //Merlin's rotation code:
            if (moveDirection.sqrMagnitude > 0f)
            {
                targetRotation = Quaternion.LookRotation(moveDirection);
                tankBody.transform.eulerAngles = Vector3.up * Mathf.MoveTowardsAngle(transform.eulerAngles.y, targetRotation.eulerAngles.y, maxRotationSpeed * Time.deltaTime);
            }
         //*
    }

    void WallDetection()
    {
        //update movement path:
        movementPath.origin = transform.position;
        movementPath.direction = moveDirection;

        //If the movementPath intersects a wall start reversing:
        if (Physics.Raycast(movementPath, out objectHit, 2f))
        {
            if (objectHit.transform.tag == "Wall")
            {
                movementPath.direction = -moveDirection;
                moveDirection = -moveDirection;
            }
        }

        //Drawing the movement path for debugging:
        Debug.DrawRay(transform.position, moveDirection * 2f);
        
    }
}
