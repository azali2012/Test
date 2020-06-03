using System.Collections;
using System.Collections.Generic;
using UnityEngine;

//Class: Enemy Shooting (Tracking), controls the enemy shooting behaviour and tracks the player when it is detected.
///Several other enemy shooting styles are adapted from this script.
///Author: Lewis Nicoll, Merlin Aldrick
public class TankOneController : MonoBehaviour
{
    //Public Game Objects:
    public GameObject bulletObject;
    public GameObject landmine;
    public Component cannonPivot;
    public Component tankBody;
    public float maxRotationSpeed = 720.0f;

    //Private Class Variables:
    private Vector3 moveInput;
    private float moveSpeed;


    private float timeBetweenShots;
    private float timePassed;
    private Quaternion targetRotation;



    // Start is called before the first frame update
    void Start()
    {
        //Initialising the variables:
        moveSpeed = 10f;

        timeBetweenShots = 0.15f;
        timePassed = 0f;
    }

    // Update is called once per frame
    void Update()
    {
        Movement();
        RightStick();
    }
    //Movement Function:
    void Movement()
    {
        //Getting the input from the controller:
        moveInput = new Vector3(Input.GetAxisRaw("HorizontalJ1"), 0.0f, Input.GetAxisRaw("VerticalJ1"));

        transform.position = transform.position + moveInput * moveSpeed * Time.deltaTime;
        if (moveInput.sqrMagnitude > 0f)
        {
            targetRotation = Quaternion.LookRotation(moveInput);
            tankBody.transform.eulerAngles = Vector3.up * Mathf.MoveTowardsAngle(transform.eulerAngles.y, targetRotation.eulerAngles.y, maxRotationSpeed * Time.deltaTime);
        }


    }

    //Rotating Function:
    void RightStick()
    {
        //Getting input from controller:
        Vector3 playerDirection = new Vector3(Input.GetAxisRaw("RHorizontalJ1"), 0f, -Input.GetAxisRaw("RVerticalJ1"));

        //Checking for any input at all:
        if (playerDirection.sqrMagnitude > 0f)
        {
            //rotating player:
            cannonPivot.transform.rotation = Quaternion.LookRotation(playerDirection, Vector3.up);
            Debug.DrawLine(transform.position, transform.position + playerDirection * 5f);

            //Shooting:
            if (Input.GetButtonDown("Shoot") && timePassed > timeBetweenShots)
            {
                timePassed = 0;

                Instantiate(bulletObject, transform.position + 1.5f * playerDirection, Quaternion.LookRotation(playerDirection, Vector3.up));

            }


        }

        if (Input.GetButtonDown("PlaceLandmine"))
        {

            Instantiate(landmine, transform.position, Quaternion.LookRotation(playerDirection, Vector3.up));
        }

        //updating timePassed:
        timePassed += Time.deltaTime;

    }

}