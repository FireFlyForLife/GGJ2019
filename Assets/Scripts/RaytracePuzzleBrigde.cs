using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class RaytracePuzzleBrigde : MonoBehaviour {
    // debug key: '1'
    private bool isRedPressed;
    // debug key: '2'
    private bool isGreenPressed;
    // debug key: '3'
    private bool isBluePressed;

    private bool isDirty;

    public bool IsRedPressed
    {
        get
        {
            return isRedPressed;
        }

        set
        {
            isRedPressed = value;
            isDirty = true;
        }
    }

    public bool IsGreenPressed
    {
        get
        {
            return isGreenPressed;
        }

        set
        {
            isGreenPressed = value;
            isDirty = true;
        }
    }

    public bool IsBluePressed
    {
        get
        {
            return isBluePressed;
        }

        set
        {
            isBluePressed = value;
            isDirty = true;
        }
    }

    // Use this for initialization
    void Start () {
		
	}
	
	// Update is called once per frame
	void Update () {
        if (Input.GetKeyDown(KeyCode.Alpha1))
        {
            IsRedPressed = (!IsRedPressed);
        }
        else if(Input.GetKeyDown(KeyCode.Alpha2))
        {
            IsGreenPressed = (!IsGreenPressed);
        }
        else if(Input.GetKeyDown(KeyCode.Alpha3))
        {
            IsBluePressed = (!IsBluePressed);
        }

        bool hasChanged = isDirty;

        if (hasChanged)
        {
            StartCoroutine(ToggleThingsWithDelay());
           
            isDirty = false;
        }
	}

    IEnumerator ToggleThingsWithDelay()
    {
        yield return new WaitForSeconds(0.3f);

        var platforms = FindObjectsOfTypeAll<TogglingPlatform>();
        foreach (var platform in platforms)
        {
            platform.TogglePlatform(IsRedPressed, IsGreenPressed, IsBluePressed);
        }
    }

    // From: http://answers.unity.com/answers/1272001/view.html
    public static List<T> FindObjectsOfTypeAll<T>()
    {
        List<T> results = new List<T>();
        for (int i = 0; i < SceneManager.sceneCount; i++)
        {
            var s = SceneManager.GetSceneAt(i);
            if (s.isLoaded)
            {
                var allGameObjects = s.GetRootGameObjects();
                for (int j = 0; j < allGameObjects.Length; j++)
                {
                    var go = allGameObjects[j];
                    results.AddRange(go.GetComponentsInChildren<T>(true));
                }
            }
        }
        return results;
    }
}
