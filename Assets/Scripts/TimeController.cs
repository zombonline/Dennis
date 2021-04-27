using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class TimeController : MonoBehaviour
{
    public float smoothRate;
    public float smoothDecrease;
    public bool slowTimeToStop = false;
    public bool slowTimeToSetAmount = false;
    public float slowMoRate= 0.2f;

    public float time;

    private void Update()
    {
        time = Time.timeScale;
        SlowTimeToStop();
        SlowTimeToSetAmount();

        if(!slowTimeToSetAmount && !slowTimeToStop)
        {
            smoothRate = 1;
        }
    }

    public void SlowTimeToStop()
    {
        if (slowTimeToStop)
        {
            smoothRate -= smoothDecrease * Time.fixedDeltaTime;


            if (smoothRate <= 0)
            {
                Time.timeScale = 0;
                slowTimeToStop = false;
            }
            else
            {
                Time.timeScale = smoothRate;
            }
        }
    }

    public void SlowTimeToSetAmount()
    {
        if (slowTimeToSetAmount)
        {
            smoothRate -= smoothDecrease * Time.fixedDeltaTime;


            if (smoothRate <= slowMoRate)
            {
                Time.timeScale = slowMoRate;
                slowTimeToSetAmount = false;
            }
            else
            {
                Time.timeScale = smoothRate;
            }
        }
    }

    public void ResetTime()
    {
        Time.timeScale = 1f;
    }



}
