using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Cooldown {
    private float activatedTime;
    private float delay;

    public float Delay
    {
        get
        {
            return delay;
        }

        set
        {
            delay = value;
        }
    }

    public Cooldown()
    {
        activatedTime = Time.time;
        delay = 0f;
    }

    public Cooldown(float delay)
    {
        activatedTime = Time.time;
        this.delay = delay;
    }

    public bool TryToActivate()
    {
        float time = Time.time;

        if(activatedTime + delay < time)
        {
            activatedTime = time;
            return true;
        }

        return false;
    }

    public bool CheckCooldown()
    {
        float time = Time.time;
        return activatedTime + delay < time;
    }
}
