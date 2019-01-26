using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class SmoothCameraFollow : MonoBehaviour {
    public Transform TargetTransform;

    public bool DontUseOffset = false;

    [HideInInspector]
    public Vector3 offset = new Vector3();

	// Use this for initialization
	void Start () {
        if(!DontUseOffset)
            offset = transform.position - TargetTransform.position;

    }
	
	// Update is called once per frame
	void Update () {
		
	}

    void LateUpdate()
    {
        transform.position = TargetTransform.position + offset;
    }
}
