using System.Collections;
using System.Collections.Generic;
using System.Linq;
using UnityEngine;

public class TogglingPlatform : MonoBehaviour {
    public bool IsRed;
    public bool IsGreen;
    public bool IsBlue;

    public bool StartsEnabled = true;

    public float DisabledTransparency = 0.35f;

    [SerializeField]
    private bool IsCurrentlyEnabled;

    private MeshRenderer meshRenderer;
    private new Collider collider;

    // Use this for initialization
    void Start () {
        meshRenderer = GetComponent<MeshRenderer>();
        collider = GetComponent<Collider>();
        //if (enabled != StartsEnabled)
        //    Debug.LogWarning("Toggling platform has it's gameobject mismatched from it's StartEnabled flag!", this);

        //gameObject.SetActive(StartsEnabled);

        IsCurrentlyEnabled = StartsEnabled;
        if (!IsCurrentlyEnabled)
        {
            var col = meshRenderer.material.color;
            col.a = DisabledTransparency;
            meshRenderer.material.color = col;

            collider.enabled = false;
        }
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
        
        if (IsCurrentlyEnabled && !ShouldBeOn)
        {
            //gameObject.SetActive(StartsEnabled);
            var col = meshRenderer.material.color;
            col.a = StartsEnabled ? 1.0f : DisabledTransparency;
            meshRenderer.material.color = col;

            collider.enabled = StartsEnabled;
            IsCurrentlyEnabled = false;
        }
        else if(ShouldBeOn)
        {
            var col = meshRenderer.material.color;
            col.a = !StartsEnabled ? 1.0f : DisabledTransparency;
            meshRenderer.material.color = col;

            collider.enabled = !StartsEnabled;

            IsCurrentlyEnabled = true;
        }
    }
}
