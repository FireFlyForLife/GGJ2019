using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ENDGAMETHING : MonoBehaviour {

	// Use this for initialization
	void Start () {
		
	}
	
	// Update is called once per frame
	void Update () {
		
	}

    void OnTriggerEnter(Collider col)
    {
        var pm = col.GetComponent<PlayerManager>();
        if(pm)
        {
            pm.PlayerUI.WinText.gameObject.SetActive(true);
        }
    }
}
