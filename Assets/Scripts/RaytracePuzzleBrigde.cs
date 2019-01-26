using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class RaytracePuzzleBrigde : MonoBehaviour {
    // debug key: '1'
    public bool IsRedPressed;
    // debug key: '2'
    public bool IsGreenPressed;
    // debug key: '3'
    public bool IsBluePressed;

    // Use this for initialization
    void Start () {
		
	}
	
	// Update is called once per frame
	void Update () {
        bool hasChanged = true;
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
        else
        {
            hasChanged = false;
        }

        if(hasChanged)
        {
           var platforms = FindObjectsOfTypeAll<TogglingPlatform>();
           Debug.Log(platforms.Count);
           foreach(var platform in platforms)
           {
                platform.TogglePlatform(IsRedPressed, IsGreenPressed, IsBluePressed);
           }
        }
	}

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
