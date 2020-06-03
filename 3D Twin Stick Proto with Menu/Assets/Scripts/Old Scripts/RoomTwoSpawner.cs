using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class RoomTwoSpawner : MonoBehaviour
{

    public GameObject enemies;
    public GameObject floorOneDoor;
    public GameObject floorTwoDoor;
    public Transform spawnTwo;
    public Transform spawnOne;
    // Start is called before the first frame update
    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {
        
    }

    void OnTriggerEnter(Collider other)
    {
        Instantiate(enemies, spawnOne);
        Instantiate(enemies, spawnTwo);
        floorOneDoor.gameObject.SetActive(true);
        floorTwoDoor.gameObject.SetActive(true);
        Destroy(gameObject);
        GameMode.enemiesLeft += 2;
    }
}
