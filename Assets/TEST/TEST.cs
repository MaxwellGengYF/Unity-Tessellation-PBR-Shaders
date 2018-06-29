using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Rendering;

public class TEST : MonoBehaviour {
    private Material mat;
    public Renderer rend;
    public MeshFilter filter;
    public RenderTexture rt;
    CommandBuffer b;
    // Will be called from camera after regular rendering is done.
    public void Start()
    {
        if (!mat)
        {
            // Unity has a built-in shader that is useful for drawing
            // simple colored things. In this case, we just want to use
            // a blend mode that inverts destination colors.
            var shader = Shader.Find("Unlit/NewUnlitShader");
            mat = new Material(shader);
        }
        b = new CommandBuffer();
    }

    private void OnPostRender()
    {
        Mesh m = filter.sharedMesh;
        Matrix4x4 mx = filter.transform.localToWorldMatrix;
        Graphics.SetRenderTarget(rt);
        GL.Clear(true, true, Color.green);
        for (int i = 0; i < 8192; ++i) {
            mat.SetPass(0);
            Graphics.DrawMeshNow(m, mx);
        }
    }
}
