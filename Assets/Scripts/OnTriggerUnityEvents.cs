using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Events;

public class OnTriggerUnityEvents : MonoBehaviour {
    public UnityEvent EventTriggerEnter;
    public UnityEvent EventTriggerExit;

    void OnTriggerEnter(Collider col)
    {
        EventTriggerEnter.Invoke();
    }

    void OnTriggerExit(Collider col)
    {
        EventTriggerExit.Invoke();
    }
}
