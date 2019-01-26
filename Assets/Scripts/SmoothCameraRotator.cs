using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class SmoothCameraRotator : MonoBehaviour {
    public float TargetYRotation = 0.0f;
    public float DeltaRotation = 2f;

	void Start () {
		
	}
	
	// Update is called once per frame
	void Update () {
		if(!Mathf.Approximately( Mathf.DeltaAngle( transform.rotation.eulerAngles.y, TargetYRotation), 0f) )
        {
            print("Rotating...");
            transform.rotation = Quaternion.RotateTowards(transform.rotation, Quaternion.Euler(0, TargetYRotation, 0), DeltaRotation);
        }
	}
}
