using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class LevelOneController : MonoBehaviour
{
    public GameObject door;


    // Start is called before the first frame update
    void Start()
    {
     

    }

    // Update is called once per frame
    void Update()
    {

     if(GameMode.enemiesLeft <= 0)
        {
            SceneManager.LoadScene("ClassicLevel2");
        }

    }
}
