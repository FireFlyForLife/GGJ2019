using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class TurnCamera : MonoBehaviour {
    public bool TurnsRight;
    public bool TurnsLeft;

    private PlayerManager containingPlayer;

	// Use this for initialization
	void Start () {
        if (!TurnsLeft && !TurnsRight)
            Debug.LogWarning("Both TurnsRight and TurnsLeft are disabled, please select one of them!", this);
        if(TurnsLeft && TurnsRight)
            Debug.LogWarning("Both TurnsRight and TurnsLeft are enabled, please only select one of them!", this);


    }

    // Update is called once per frame
    void Update () {
		
	}

    IEnumerator Turn()
    {
        if(TurnsLeft)
        {
            var rotation = containingPlayer.PlayerCamera.transform.rotation;
           // rotation = Quaternion.RotateTowards(rotation, )

            yield return null;
        }
        else //TurnsRight
        {

        }
    }

    void OnTriggerEnter(Collider col)
    {
        var possiblePlayer = col.GetComponent<PlayerManager>();
        if(possiblePlayer)
        {
            if (containingPlayer != possiblePlayer)
            {
                containingPlayer = possiblePlayer;
                Turn();
            }
        }
    }

    void OnTriggerExit(Collider col)
    {
        var possiblePlayer = col.GetComponent<PlayerManager>();
        if (possiblePlayer)
        {
            if (containingPlayer == possiblePlayer)
                containingPlayer = null;
        }
    }
}
