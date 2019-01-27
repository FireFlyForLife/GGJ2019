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

    private float rotationStart;
    public float RotationAmount = 0f;

    private bool IsCurrentlyEnabled;

    private MeshRenderer meshRenderer;
    private new Collider collider;

    private Coroutine coroutine;

    // Use this for initialization
    void Start () {
        meshRenderer = GetComponentInChildren<MeshRenderer>();
        collider = GetComponent<Collider>();

        rotationStart = transform.localEulerAngles.y;

        //if (enabled != StartsEnabled)
        //    Debug.LogWarning("Toggling platform has it's gameobject mismatched from it's StartEnabled flag!", this);

        //gameObject.SetActive(StartsEnabled);

        IsCurrentlyEnabled = StartsEnabled;
        if (!IsCurrentlyEnabled)
        {
            var col = meshRenderer.material.color;
            col.a = DisabledTransparency;
            meshRenderer.material.color = col;

            if(collider)
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

    private IEnumerator RotateDoor()
    {
        float target;
        if (IsCurrentlyEnabled)
            target = rotationStart + RotationAmount;
        else
            target = rotationStart;

        while (!Mathf.Approximately(transform.localEulerAngles.y, target))
        {
            var yrot = transform.localEulerAngles.y;
            yrot = Mathf.MoveTowardsAngle(yrot, target, 0.5f);

            var eulerRot = transform.localEulerAngles;
            eulerRot.y = yrot;
            transform.localEulerAngles = eulerRot;

            yield return null;
        }
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

            if(collider)
                collider.enabled = StartsEnabled;
            IsCurrentlyEnabled = false;

            if(coroutine != null)
                StopCoroutine(coroutine);
            coroutine = StartCoroutine(RotateDoor());
        }
        else if(ShouldBeOn)
        {
            var col = meshRenderer.material.color;
            col.a = !StartsEnabled ? 1.0f : DisabledTransparency;
            meshRenderer.material.color = col;

            if(collider)
                collider.enabled = !StartsEnabled;

            IsCurrentlyEnabled = true;
            if(coroutine != null)
                StopCoroutine(coroutine);
            coroutine = StartCoroutine(RotateDoor());

        }
    }
}
