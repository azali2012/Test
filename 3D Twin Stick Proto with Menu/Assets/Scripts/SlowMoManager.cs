using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class SlowMoManager
{
    private float defaultSlowTimeScale;
    public bool isSlowMo;

    // Start is called before the first frame update
    void Start()
    {
        defaultSlowTimeScale = 0.1f;
        isSlowMo = false;
    }

    // Update is called once per frame
    public void Update()
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

    void SlowTime(float timeScale)
    {
        Time.timeScale = timeScale;
        Time.fixedDeltaTime = Time.timeScale * 0.02f;
    }

    void ResetTime()
    {
        Time.timeScale = 1f;
    }
}
