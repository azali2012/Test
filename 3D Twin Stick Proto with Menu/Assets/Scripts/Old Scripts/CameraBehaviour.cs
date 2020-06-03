using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CameraBehaviour : MonoBehaviour
{
    //public references:
    public GameObject playerRB;

    //private variables:
    private float cameraHeight;

    // Start is called before the first frame update
    void Start()
    {
        cameraHeight = 7.5f;
    }

    // Update is called once per frame
    void Update()
    {
        CameraMovement();
    }

    //Camera movement i.e camera following the player:
    void CameraMovement()
    {
        transform.position = playerRB.transform.position + new Vector3(0f, cameraHeight, -10f);
    }
}
