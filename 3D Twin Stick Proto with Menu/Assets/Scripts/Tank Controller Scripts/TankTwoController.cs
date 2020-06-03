using System.Collections;
using System.Collections.Generic;
using UnityEngine;


public class TankTwoController : MonoBehaviour
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
        moveInput = new Vector3(Input.GetAxisRaw("HorizontalJ2"), 0.0f, Input.GetAxisRaw("VerticalJ2"));

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
        Vector3 playerDirection = new Vector3(Input.GetAxisRaw("RHorizontalJ2"), 0f, -Input.GetAxisRaw("RVerticalJ2"));

        //Checking for any input at all:
        if (playerDirection.sqrMagnitude > 0f)
        {
            //rotating player:
            cannonPivot.transform.rotation = Quaternion.LookRotation(playerDirection, Vector3.up);
            Debug.DrawLine(transform.position, transform.position + playerDirection * 5f);

            //Shooting:
            if (Input.GetButtonDown("TwoShoot") && timePassed > timeBetweenShots)
            {
                timePassed = 0;

                Instantiate(bulletObject, transform.position + 1.5f * playerDirection, Quaternion.LookRotation(playerDirection, Vector3.up));

            }


        }

        if (Input.GetButtonDown("TwoPlaceLandmine"))
        {

            Instantiate(landmine, transform.position, Quaternion.LookRotation(playerDirection, Vector3.up));
        }


        timePassed += Time.deltaTime;

    }

    //void DropMine()
    //    {


    //        if (Input.GetButtonDown("PlaceLandmine"))
    //        {

    //            Instantiate(landmine, transform.position + 1.5f * playerDirection, Quaternion.LookRotation(playerDirection, Vector3.up));
    //        }
    //    }

}