using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class RoomFourSpawner : MonoBehaviour
{

    public GameObject enemies;
    public GameObject finalDoor;
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
        finalDoor.gameObject.SetActive(true);
        Destroy(gameObject);
        GameMode.enemiesLeft += 2;
    }
}

