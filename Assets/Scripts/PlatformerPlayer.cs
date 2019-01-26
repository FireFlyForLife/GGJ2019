using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlatformerPlayer : MonoBehaviour {
    public float JumpSpeed = 10f;
    public float MovementSpeed = 5f;
    public Vector2 MaxSpeed = new Vector2(10f, 10f);
    public float DownwardsForcePerSecond = 2.5f;

    private Rigidbody playerRigidbody;
    private PlayerOpenMap playerOpenMap;
    private CapsuleCollider capsuleCollider;

    private bool canJump = true;

	// Use this for initialization
	void Start () {
        playerRigidbody = GetComponent<Rigidbody>();
        playerOpenMap = GetComponent<PlayerOpenMap>();
        capsuleCollider = GetComponent<CapsuleCollider>();
    }
	
	// Update is called once per frame
	void Update () {
        if (playerOpenMap.IsMapOpened)
        {
            var velocity = playerRigidbody.velocity;
            velocity.x = 0f;
            velocity.z = 0f;
            playerRigidbody.velocity = velocity;
            return;
        }

        float hor = Input.GetAxis("Horizontal");
        float jump = Input.GetAxisRaw("Jump");
        {
            var velocity = playerRigidbody.velocity;
            if (Math.Abs(Camera.main.transform.localEulerAngles.y % 90.0) < 0.0001)
                velocity = Camera.main.transform.right * hor * MovementSpeed + new Vector3(0, velocity.y, 0);
            else
            {
                velocity.x = 0;
                velocity.z = 0;
            }
            playerRigidbody.velocity = velocity;
        }
        if(jump == 1f && canJump)
        {
            playerRigidbody.AddForce(new Vector3(0f, JumpSpeed, 0f) * JumpSpeed, ForceMode.Impulse);
            canJump = false;
        }

        playerRigidbody.AddForce(new Vector3(0f, -DownwardsForcePerSecond, 0f));

        //Limit speed
        var vel = playerRigidbody.velocity;
        vel.x = Mathf.Clamp(vel.x, -MaxSpeed.x, MaxSpeed.x);
        vel.y = Mathf.Clamp(vel.y, -9999999f, MaxSpeed.y);
        playerRigidbody.velocity = vel;
    }

    private void OnCollisionEnter(Collision collision)
    {
        var lowerY = capsuleCollider.bounds.min.y;
        foreach(var contact in collision.contacts)
        {
            if (contact.point.y <= lowerY)
                canJump = true;
        }

        //canJump = true;
    }

    private void OnCollisionStay(Collision collision)
    {
        var lowerY = capsuleCollider.bounds.min.y;
        foreach (var contact in collision.contacts)
        {
            if (contact.point.y <= lowerY)
                canJump = true;
        }
    }

    private void OnCollisionExit(Collision collision)
    {
    }
}
