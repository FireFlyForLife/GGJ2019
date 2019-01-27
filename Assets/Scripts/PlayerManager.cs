﻿using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityStandardAssets.Characters.ThirdPerson;
public enum PlayerAxis
{
    X,
    Z
}

[RequireComponent(typeof(HealthComponent), typeof(ThirdPersonUserControl))]
public class PlayerManager : MonoBehaviour {
    private HealthComponent health;
    private ThirdPersonUserControl userControl;
    private ThirdPersonCharacter thirdPersonCharacter;
    [SerializeField]
    private PlayerUI playerUI;
    private Camera playerCamera;
    private PlayerAxis playerAxis = PlayerAxis.X;
    private PlayerOpenMap playerOpenMap;
    private Rigidbody playerRigidbody;

    public Camera PlayerCamera
    {
        get
        {
            return playerCamera;
        }
    }
    public PlayerUI PlayerUI
    {
        get
        {
            return playerUI;
        }
    }
    public ThirdPersonUserControl UserControl
    {
        get
        {
            return userControl;
        }
    }

    public HealthComponent Health
    {
        get
        {
            return health;
        }
    }

    public PlayerAxis PlayerAxis
    {
        get
        {
            return playerAxis;
        }
        set
        {
            playerAxis = value;
        }
    }

    public PlayerOpenMap PlayerOpenMap
    {
        get
        {
            return playerOpenMap;
        }
    }

    public ThirdPersonCharacter ThirdPersonCharacter
    {
        get
        {
            return thirdPersonCharacter;
        }
    }

    public Rigidbody PlayerRigidbody
    {
        get
        {
            return playerRigidbody;
        }
    }

    // Use this for initialization
    void Start () {
        health = GetComponent<HealthComponent>();
        userControl = GetComponent<ThirdPersonUserControl>();
        thirdPersonCharacter = GetComponent<ThirdPersonCharacter>();
        playerOpenMap = GetComponent<PlayerOpenMap>();
        playerRigidbody = GetComponent<Rigidbody>();

        playerCamera = Camera.main;
    }
	
	// Update is called once per frame
	void Update () {
		if(!health.IsAlive())
        {
            userControl.enabled = false;

            var vel = GetComponent<Rigidbody>().velocity;
            vel.x = 0;
            GetComponent<Rigidbody>().velocity = vel;

            playerUI.DeadText.gameObject.SetActive(true);
        }
        
        userControl.HandleInput = (!playerOpenMap.IsMapOpened);
        thirdPersonCharacter.HandleInput = (!playerOpenMap.IsMapOpened);
        playerRigidbody.isKinematic = playerOpenMap.IsMapOpened;

    }
}
