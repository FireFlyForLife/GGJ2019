using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class RTScript : MonoBehaviour
{
    [SerializeField] private Material mat;
	// Use this for initialization
	void Start () {
		
	}
	
	// Update is called once per frame
	void Update ()
	{
	    List<Vector4> positons = new List<Vector4>();
        for(int i = 0; i < 5; ++i)
            positons.Add(new Vector4(0,0,i*2,0));
		mat.SetVectorArray("_Positions", positons);
		mat.SetInt("_Size", 1);
	}
}
