using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class GPUDataGrabber : MonoBehaviour {
    [SerializeField]
    private RenderTexture RenderTexture;
    [SerializeField]
    private RenderTexture ShaderRenderTexture;

    private RawImage uiImage;
    private Texture2D tex;


    // Use this for initialization
    void Start () {
        Debug.Log("STARTING THE GPU DATA GRABBER!!");

        RenderTexture = new RenderTexture(1024, 1024, 0, RenderTextureFormat.ARGB32, RenderTextureReadWrite.Linear);

        tex = new Texture2D(RenderTexture.width, RenderTexture.height, TextureFormat.ARGB32, false);

        RenderTexture.enableRandomWrite = true;

        uiImage = GetComponent<RawImage>();
    }

    public void UpdateTextureCopy()
    {
        Rect rectReadPicture = new Rect(0, 0, RenderTexture.width, RenderTexture.height);

        var texture = uiImage.texture;
        var material = uiImage.material;

        Graphics.Blit(texture, RenderTexture, material, -1);

        RenderTexture.active = RenderTexture;
        tex.ReadPixels(rectReadPicture, 0, 0);
        tex.Apply();
        RenderTexture.active = null;
    }

    // Update is called once per frame
    void Update () {
        UpdateTextureCopy();

        //Color c = tex.GetPixel(0, 1023);
        //Debug.Log("First color: " + c.ToString(), this);

        //c = tex.GetPixel(1, 1023);
        //Debug.Log(" color: " + c.ToString(), this);

        //c = tex.GetPixel(5, 1023);
        //Debug.Log(" color: " + c.ToString(), this);

        //c = tex.GetPixel(6, 1023);
        //Debug.Log(" color: " + c.ToString(), this);
    }
}
