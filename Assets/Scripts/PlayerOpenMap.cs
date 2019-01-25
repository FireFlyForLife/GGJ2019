using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerOpenMap : MonoBehaviour {
    public Camera RaytraceCamera;
    public bool IsMapOpened;

	// Use this for initialization
	void Start () {
        IsMapOpened = RaytraceCamera.enabled;
    }
	
	// Update is called once per frame
	void Update () {
        if(Input.GetButtonDown("Toggle Map"))
        {
            IsMapOpened = (!IsMapOpened);
            RaytraceCamera.enabled = IsMapOpened;
        }
        
    }
}
