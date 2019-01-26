using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class GPUDataGrabber : MonoBehaviour {
    [SerializeField]
    private RenderTexture RenderTexture;
    private RenderTexture RenderTexture2;
    [SerializeField]
    private RenderTexture ShaderRenderTexture;

    private RawImage uiImage;
    private Texture2D tex;

    private ComputeBuffer buffer;

    // Use this for initialization
    void Start () {
        Debug.Log("STARTING THE GPU DATA GRABBER!!");
        buffer = new ComputeBuffer(20, 4, ComputeBufferType.Default);

        RenderTexture2 = new RenderTexture(1024*4, 1024*4, 0, RenderTextureFormat.ARGB32, RenderTextureReadWrite.Linear);

        tex = new Texture2D(1024, 1024, TextureFormat.ARGB32, false, true);

        RenderTexture2.enableRandomWrite = true;

        uiImage = GetComponent<RawImage>();
    }

    public void UpdateTextureCopy()
    {
        Graphics.ClearRandomWriteTargets();

        Rect rectReadPicture = new Rect(0, 0, RenderTexture2.width, RenderTexture2.height);

        var texture = uiImage.texture;
        var material = uiImage.material;
        material.SetBuffer("_OutBuffer", buffer);

        Graphics.SetRandomWriteTarget(1, buffer);
        Graphics.Blit(texture, material, 0);

        float[] data = new float[20];
        buffer.GetData(data);
        for (int i = 0; i < data.Length; i++)
        {
            print(i + ":  " + data[i]);
        }

        //RenderTexture.active = RenderTexture2;
        ////tex.ReadPixels(rectReadPicture, 0, 0);
        //tex.Apply();
        //RenderTexture.active = null;


        //Graphics.Blit(RenderTexture2, ShaderRenderTexture);
    }

    // Update is called once per frame
    void Update () {
        UpdateTextureCopy();

        Color c = tex.GetPixel(0, 0);
        Debug.Log("First color: " + c.ToString(), this);

        c = tex.GetPixel(1, 1023);
        Debug.Log(" color: " + c.ToString(), this);

        c = tex.GetPixel(5, 1023);
        Debug.Log(" color: " + c.ToString(), this);

        c = tex.GetPixel(6, 1023);
        Debug.Log(" color: " + c.ToString(), this);
    }
}
