using System.Collections;
using System.Collections.Generic;
using System.Linq;
using UnityEngine;

public class TogglingPlatform : MonoBehaviour {
    public bool IsRed;
    public bool IsGreen;
    public bool IsBlue;

    public bool StartsEnabled = true;

    // Use this for initialization
    void Start () {
        if (enabled != StartsEnabled)
            Debug.LogWarning("Toggling platform has it's gameobject mismatched from it's StartEnabled flag!", this);
		
		gameObject.SetActive(StartsEnabled);
	}
	
	// Update is called once per frame
	void Update () {
		
	}

    private bool ShouldTurnOn(bool[] inputBools, bool[] ourBools)
    {
        for(int i = 0; i < inputBools.Length; ++i)
        {
            if (ourBools[i] && inputBools[i] != ourBools[i])
                return false;
        }

        return true;
    }

    public void TogglePlatform(bool red, bool green, bool blue)
    {
        bool[] inputBools = new bool[] { red, green, blue };
        bool[] ourBools = new bool[] { IsRed, IsGreen, IsBlue };

        bool ShouldBeOn = ShouldTurnOn(inputBools, ourBools);

        var isEnabled = enabled;
        if (isEnabled && !ShouldBeOn)
        {
            gameObject.SetActive(StartsEnabled);
        }
        else if(ShouldBeOn)
        {
            gameObject.SetActive(!StartsEnabled);
        }
    }
}
