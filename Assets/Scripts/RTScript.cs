using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class RTScript : MonoBehaviour
{
    [SerializeField] private Material mat;

    private ComputeBuffer buffer;

	void Start () {
        buffer = new ComputeBuffer(3, sizeof(float), ComputeBufferType.Default);
	}
	
	// Update is called once per frame
	void Update ()
	{

	}

    void OnRenderObject()
    {
        Graphics.ClearRandomWriteTargets();
        List<Vector4> positons = new List<Vector4>();
        for (int i = 0; i < 5; ++i)
            positons.Add(new Vector4(0, 0, i * 4, 0));
        mat.SetVectorArray("_Positions", positons);
        mat.SetInt("_Size", 2);
        mat.SetBuffer("_OutBuffer", buffer);
        Graphics.SetRandomWriteTarget(1, buffer);

        float[] floats = new float[3];
        buffer.GetData(floats);
        for (int i = 0; i < floats.Length; i++)
        {
            print(mat.name + " : " + i + ":  " + floats[i]);
        }
    }
}
