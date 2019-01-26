using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerOpenMap : MonoBehaviour {
    private RTScript rtScript;
    public GameObject RaytraceCamera;
    public bool IsMapOpened;

	// Use this for initialization
	void Start ()
	{
	    rtScript = FindObjectOfType<RTScript>();
        IsMapOpened = RaytraceCamera.activeSelf;
	    rtScript.RTEnabled = IsMapOpened;
    }
	
	// Update is called once per frame
    void Update()
    {
        if (Input.GetButtonDown("Toggle Map"))
        {
            IsMapOpened = !IsMapOpened;
            rtScript.RTEnabled = IsMapOpened;
            //RaytraceCamera.SetActive(IsMapOpened);
            RaytraceCamera.transform.localPosition = new Vector3(0, 0, -3 * (IsMapOpened ? 1:0));
        }
    }
}
