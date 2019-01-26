using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Killzone : MonoBehaviour {
    void OnTriggerEnter(Collider collider)
    {
        var health = collider.GetComponent<HealthComponent>();
        if(health)
        {
            health.DirectlyKill();
        }
    }

    void OnTriggerExit(Collider collider)
    {

    }

    void OnCollisionEnter(Collision collision)
    {
        var health = collision.collider.GetComponent<HealthComponent>();
        if (health)
        {
            health.DirectlyKill();
        }
    }

    void OnCollisionExit(Collision collision)
    {

    }
}
