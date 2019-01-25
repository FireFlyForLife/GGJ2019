using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class SmoothCameraFollow : MonoBehaviour {
    public Transform TargetTransform;
    public float Speed = 5f;

    private Vector3 offset;

	// Use this for initialization
	void Start () {
        offset = transform.position - TargetTransform.position;

    }
	
	// Update is called once per frame
	void Update () {
		
	}

    void LateUpdate()
    {
        //Mathf.SmoothStep

        var pos = transform.position;
        transform.position = Vector3.MoveTowards(pos, TargetTransform.position + offset, Speed);
    }
}
