using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class GameManager : MonoBehaviour
{
    //Create an instance of the game manager:
    static GameManager instance;

    //Get function to return an a reference of the game manager:
    public static GameManager GetGameManager()
    {
        return instance;
    }

    private Camera  camera;
    private Vector3 m_ogCamPosition;
    private float   m_ogCamOrthographicSize;

    private SlowMoManager slowMoManager;

    public SlowMoManager getTimeManager()
    {
        return slowMoManager;
    }

    private float defaultSlowTimeScale;
    public bool isSlowMo;


    // Awake is called befoee anything else:
    void Awake()
    {
        if(instance != null)
        {
            Destroy(this.gameObject);
        }
        else if (instance == null)
        {
            instance = this;
            GameObject.DontDestroyOnLoad(this.gameObject);
        }
        
    }

    //Start is called before the first frame of update:
    void Start()
    {
        slowMoManager           = new SlowMoManager();

        defaultSlowTimeScale    = 0.1f;
        isSlowMo                = false;

        camera                  = Camera.main;
        m_ogCamPosition         = camera.transform.position;
        m_ogCamOrthographicSize = camera.orthographicSize;
    }

    // Update is called once per frame
    void Update()
    {
        AlterTime();
    }

    void AlterTime()
    {
        if (isSlowMo)
        {
            SlowTime();
        }
        if (!isSlowMo)
        {
            ResetTime();
        }
    }
    void SlowTime()
    {
        Time.timeScale = defaultSlowTimeScale;
        Time.fixedDeltaTime = Time.timeScale * 0.02f;
        Debug.Log("SlowTime");
    }

    void SlowTime( float timeScale )
    {
        Time.timeScale = timeScale;
        Time.fixedDeltaTime = Time.timeScale * 0.02f;
    }

    void ResetTime()
    {
        Time.timeScale = 1f;
    }

    public void FollowCam(Vector3 position)
    {
        //camera = Camera.main;
        //m_ogCamPosition = camera.transform.position;
        //m_ogCamOrthographicSize = camera.orthographicSize;
        Vector3 followPos = new Vector3(position.x, position.y+10f, position.z-10f);
        camera.transform.position = followPos;
        camera.orthographicSize = 2f;
    }

    public void ResetCam()
    {
        camera.transform.position = m_ogCamPosition;
        camera.orthographicSize = m_ogCamOrthographicSize;
    }
}
