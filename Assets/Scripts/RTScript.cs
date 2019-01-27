using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class RTScript : MonoBehaviour
{
    [SerializeField] private Material mat;
    [SerializeField] private Material red, green, blue, combined;

    public float MovementSpeed = 5;
    public float RotationSpeed = 2;

    public PlayerManager PlayerManager;

    public bool RTEnabled = false;

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
        if (!PlayerManager.GetComponent<PlayerOpenMap>().IsMapOpened)
            return;

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

        if (!RTEnabled)
            return;

        List<Vector4> positons = new List<Vector4>();
        List<Vector4> colors = new List<Vector4>();
        //for (int i = 0; i < 5; ++i)
        positons.Add(new Vector4(0, 0, 0, 0));
        positons.Add(new Vector4(0, 0, 10, 0));
        positons.Add(new Vector4(0, 0, -10, 0));
        positons.Add(new Vector4(10, 0, 0, 0));
        positons.Add(new Vector4(-10, 0, 0, 0));
        positons.Add(new Vector4(0, 1.5f, 0, 0));
        colors.Add(new Vector4(0,0,0,0));
        colors.Add(new Vector4(1,0,0,0));
        colors.Add(new Vector4(0,1,0,0));
        colors.Add(new Vector4(0,0,1,0));
        colors.Add(new Vector4(1,0,1,0));
        colors.Add(new Vector4(1,1,1,0));

        if (positons.Count != colors.Count)
            Debug.LogWarning("Warning! the colors array is not the same size as positions in the raytracer!!!", this);

        mat.SetVectorArray("_Positions", positons);
        mat.SetVectorArray("_Colors", colors);
        mat.SetInt("_Size", positons.Count);
        mat.SetVector("_CamPosition", position);
        mat.SetFloat("_CamRotation", rotation * Mathf.Deg2Rad);
        mat.SetBuffer("_OutBuffer", buffer);
        Graphics.SetRandomWriteTarget(1, buffer);

        float[] floats = new float[3];
        buffer.GetData(floats);
        //for (int i = 0; i < floats.Length; i++)
        //{
        //    if(floats[i] != 0f)
        //    {
        //        print(mat.name + " : " + i + ":  " + floats[i]);
        //    }
        //}

        bool r = !Mathf.Approximately( floats[0], 0f);
        bool g = !Mathf.Approximately(floats[1] , 0f);
        bool b = !Mathf.Approximately(floats[2] , 0f);

        var bridge = PlayerManager.GetComponent<RaytracePuzzleBrigde>();
        bridge.IsRedPressed = r;
        bridge.IsGreenPressed = g;
        bridge.IsBluePressed = b;
        red.color = new Color(1, 0, 0) * (r ? 1 : 0);
        green.color = new Color(0, 1, 0) * (g ? 1 : 0);
        blue.color = new Color(0, 0, 1) * (b ? 1 : 0);
        combined.color = new Color(red.color.r, green.color.g, blue.color.b);
    }

    //void OnPostRender()
    //{
    //    print("Post Render");
    //    List<float> l = new List<float>() { 0, 0, 0 };
    //    buffer.SetData(l);
    //}
}
