using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerOpenMap : MonoBehaviour {
    public GameObject RaytraceCamera;
    public bool IsMapOpened;

	// Use this for initialization
	void Start () {
        IsMapOpened = RaytraceCamera.activeSelf;
    }
	
	// Update is called once per frame
	void Update () {
        if(Input.GetButtonDown("Toggle Map"))
        {
            IsMapOpened = (!IsMapOpened);
            RaytraceCamera.SetActive( IsMapOpened );
        }
        
    }
}
