using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerTwoMovement : MonoBehaviour
{
    //Public Game Objects:
    public GameObject   bulletObject;
    public GameObject   fireBulletObject;
    public GameObject   waterBulletObject;
    public GameObject   earthBulletObject;
    public float playerHealth;

    //Bullet Types:
    public enum bulletType
    {
        normal,
        fire,
        water,
        earth,
    };

    private bulletType bullettype;

    //Private Class Variables:
    private Rigidbody   m_rigidbody;

    private Vector3     moveInput;
    private float       moveSpeed;

    private float       timeBetweenShots;
    private float       timePassed;

    private float       timeSinceBulletSwitch;
    private float       bulletSwitchTime;

    // Start is called before the first frame update
    void Start()
    {
        bullettype = bulletType.fire;

        m_rigidbody         = GetComponent<Rigidbody>();

        moveSpeed           = 10f;

        timeBetweenShots  = 0.15f;
        timePassed        = 0f;

        timeSinceBulletSwitch = 0f;
        bulletSwitchTime = 1f; //To change this also change values in the BulletSwitching() function.


        playerHealth = 100;

    }

    // Update is called once per frame
    void Update()
    {
        Movement();
        Jumping();
        RightStick(bullettype);
        BulletSwitching();
        KillPlayer();
        ClampHealth();
    }
    //Movement Function:
    void Movement()
    {
        //Getting the input from the controller:
        moveInput = new Vector3(Input.GetAxisRaw("HorizontalJ2"),0f,Input.GetAxisRaw("VerticalJ2"));

        //adding movement to the rigidbody:
        //m_rigidbody.velocity = moveInput*moveSpeed;

        transform.position = transform.position + moveInput * moveSpeed * Time.deltaTime;
    }

    //Jumping Function:
    void Jumping()
    {
        if (Input.GetAxis("Jump") > 0f)
        {
            m_rigidbody.velocity = new Vector3(0f, 10f, 0f);
        }
    }

    //Rotating Function:
    void RightStick( bulletType bt)
    {
        //Getting input from controller:
        Vector3 playerDirection = new Vector3(Input.GetAxisRaw("RHorizontalJ2"), 0f, -Input.GetAxisRaw("RVerticalJ2"));

        //Checking for any input at all:
        if (playerDirection.sqrMagnitude > 0f)
        {
            //rotating player:
            transform.rotation = Quaternion.LookRotation(playerDirection, Vector3.up);

            //Shooting:
            if (timePassed > timeBetweenShots)
            {
                timePassed = 0;

                switch (bt)
                {
                    case bulletType.normal:
                        Instantiate(bulletObject, transform.position + playerDirection, Quaternion.LookRotation(playerDirection, Vector3.up));
                        break;
                    case bulletType.fire:
                        Instantiate(fireBulletObject, transform.position + playerDirection, Quaternion.LookRotation(playerDirection, Vector3.up));
                        break;
                    case bulletType.water:
                        Instantiate(waterBulletObject, transform.position + playerDirection, Quaternion.LookRotation(playerDirection, Vector3.up));
                        break;
                    case bulletType.earth:
                        Instantiate(earthBulletObject, transform.position + playerDirection, Quaternion.LookRotation(playerDirection, Vector3.up));
                        break;

                }
            }
        }

        timePassed += Time.deltaTime;
    }

    void BulletSwitching()
    { 
        //R1 button:
        if (Input.GetAxis("R1 Button") > 0f && timeSinceBulletSwitch>bulletSwitchTime)
        {
            bulletSwitchTime = 1f;
            switch (bullettype)
            {
                case bulletType.fire:
                bullettype = bulletType.water;
                    break;
                case bulletType.water:
                bullettype = bulletType.earth; 
                    break;
                case bulletType.earth:
                bullettype = bulletType.fire;
                    break;
            }
            timeSinceBulletSwitch = 0f;
        }
        //L1 Button:
        else if (Input.GetAxis("L1 Button") > 0f && timeSinceBulletSwitch > bulletSwitchTime)
        {
            bulletSwitchTime = 1f;
            switch (bullettype)
            {
                case bulletType.fire:
                    bullettype = bulletType.earth;
                    break;
                case bulletType.water:
                    bullettype = bulletType.fire;
                    break;
                case bulletType.earth:
                    bullettype = bulletType.water;
                    break;
            }
            timeSinceBulletSwitch = 0f;
        }
        //else
        //{
        //    bulletSwitchTime = 0f;
        //}

        timeSinceBulletSwitch += Time.deltaTime;
    }


    void DamagePlayer()
    {
        playerHealth -= 0.3f;
    }

    void OnTriggerStay(Collider other)
    {
        if (other.gameObject.tag == "Death Cube")
        {
            DamagePlayer();
        }
    }

    void OnTriggerEnter(Collider other)
    {
        if (other.gameObject.tag == "Health Pickup")
        {
            playerHealth += 50;
            Destroy(other.gameObject);
        }
    }

    void KillPlayer()
    {
        if (playerHealth <= 0)
            Destroy(gameObject);
    }

    void ClampHealth()
    {
        if (playerHealth >= 100)
            playerHealth = 100;
    }
}
