using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class RTScript : MonoBehaviour
{
    [SerializeField] private Material mat;

    public float MovementSpeed = 5;
    public float RotationSpeed = 2;

    private ComputeBuffer buffer;
    private float rotation;
    private Vector3 position;

    private Matrix4x4 viewMatrixCache;

	void Start () {
        buffer = new ComputeBuffer(3, sizeof(float), ComputeBufferType.Default);
	}
	
	// Update is called once per frame
	void Update ()
	{
        var hor = Input.GetAxis("Horizontal");
        var vert = Input.GetAxis("Vertical");
        var roll = Input.GetAxis("Roll");

        rotation += roll * RotationSpeed * Time.deltaTime;
        rotation = Mathf.Repeat(rotation, 360f);

        Matrix4x4 tempMat = new Matrix4x4();
        tempMat.SetTRS(Vector3.zero, Quaternion.AngleAxis(rotation, Vector3.up), Vector3.one);

        Vector4 forward4 = tempMat * new Vector4(hor * MovementSpeed * Time.deltaTime, 0, vert * MovementSpeed * Time.deltaTime, 1);
        position += new Vector3(forward4.x, forward4.y, forward4.z);

        //viewMatrixCache = new Matrix4x4();
        //viewMatrixCache.SetTRS(position, Quaternion.AngleAxis(rotation, Vector3.up), Vector3.one);

        List<float> l = new List<float>() { 0, 0, 0 };
        buffer.SetData(l);
    }

    void OnRenderObject()
    {
        Graphics.ClearRandomWriteTargets();


        List<Vector4> positons = new List<Vector4>();
        List<Vector4> colors = new List<Vector4>();
        for (int i = 0; i < 5; ++i)
        positons.Add(new Vector4(0, 0, i * 4, 0));
        colors.Add(new Vector4(1,0,0,0));
        colors.Add(new Vector4(0,0,0,0));
        colors.Add(new Vector4(0,1,0,0));
        colors.Add(new Vector4(0,0,1,0));
        colors.Add(new Vector4(1,0,1,0));

        mat.SetVectorArray("_Positions", positons);
        mat.SetVectorArray("_Colors", colors);
        mat.SetInt("_Size", 3);
        mat.SetVector("_CamPosition", position);
        mat.SetFloat("_CamRotation", rotation * Mathf.Deg2Rad);
        mat.SetBuffer("_OutBuffer", buffer);
        Graphics.SetRandomWriteTarget(1, buffer);

        float[] floats = new float[3];
        buffer.GetData(floats);
        for (int i = 0; i < floats.Length; i++)
        {
            if(floats[i] != 0f)
                print(mat.name + " : " + i + ":  " + floats[i]);
        }
    }

    //void OnPostRender()
    //{
    //    print("Post Render");
    //    List<float> l = new List<float>() { 0, 0, 0 };
    //    buffer.SetData(l);
    //}
}
