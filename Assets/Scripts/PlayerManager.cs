using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityStandardAssets.Characters.ThirdPerson;

[RequireComponent(typeof(HealthComponent), typeof(ThirdPersonUserControl))]
public class PlayerManager : MonoBehaviour {
    private HealthComponent health;
    private ThirdPersonUserControl userControl;
    [SerializeField]
    private PlayerUI playerUI;

    // Use this for initialization
    void Start () {
        health = GetComponent<HealthComponent>();
        userControl = GetComponent<ThirdPersonUserControl>();
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
	}
}
