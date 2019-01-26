using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class HealthComponent : MonoBehaviour {
    [SerializeField]
    private int health = 100;
    public int Health {
        get
        {
            return health;
        }
        set
        {
            health = value;
        }
    }

    public bool IsAlive()
    {
        return health > 0;
    }

    public void DirectlyKill()
    {
        health = 0;
    }
}
