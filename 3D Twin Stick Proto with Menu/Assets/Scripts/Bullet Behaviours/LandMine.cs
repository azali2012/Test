using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class LandMine : MonoBehaviour
{
    public GameObject fuseBurnout;
    public Collider cubeCollider;
    public Collider sphereCollider;
    public GameObject explosion;
    bool landMineActive = false;
    public float fuseLength;
    float timer;
  //  bool destroyLandmine = false;


    // Start is called before the first frame update
    void Start()
    {
        //disables the sphere collider and sets up the timer variables
        sphereCollider.enabled = false;
        explosion.gameObject.SetActive(false);
        timer = 0.0f;
        fuseLength = 5.0f;
    }

    // Update is called once per frame
    void Update()
    {

        //Starts the timer and triggers the landmine if it gets over a certain amount
        timer += Time.deltaTime;
        if (timer >= fuseLength)
        {
            TriggerLandmine();
        }
    }
    
    //sets up the landmine once the player is no longer over the top of it 
    void OnTriggerExit(Collider other)
    {
        landMineActive = true;
        if (timer == fuseLength)
        {
            sphereCollider.enabled = true;
            Destroy(other.gameObject);
            Invoke("DestroyLandmine", 0.1f);
        }
    }

    //blows up the landmine when it is triggered 
    void OnTriggerEnter(Collider other)
    {

        if (landMineActive == true)
        {
            sphereCollider.enabled = true;
            explosion.gameObject.SetActive(true);
            Destroy(other.gameObject);
            Invoke("DestroyLandmine", 0.1f);
        }
   
       
        
    }

    void DestroyLandmine()
    {
        Destroy(gameObject);
    }

    void TriggerLandmine()
    {
        Instantiate(fuseBurnout, transform.position, Quaternion.identity);  
    }



}




