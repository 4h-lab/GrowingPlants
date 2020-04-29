using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class something : MonoBehaviour{
    public Shader drawShader;

    private Material m;
    private Transform playert;

    private RenderTexture rtex;

    private Material drawMaterial;

    // Start is called before the first frame update
    void Start(){
        playert = GameObject.FindGameObjectWithTag("Player").transform;

        drawMaterial = new Material(drawShader);

        m = gameObject.GetComponent<Renderer>().material;        
        rtex = new RenderTexture(1024, 1024, 0, RenderTextureFormat.ARGBFloat);
        m.SetTexture("_MainTex", rtex);

        //rtex =(RenderTexture) m.GetTexture("_MainTex");

        m.SetVector("_ColorTint", Color.blue);
        
    }

    // Update is called once per frame
    void Update(){
        m.SetFloat("_PlayerPosX", playert.position.x);
        m.SetFloat("_PlayerPosY", playert.position.y);
        
        
        RenderTexture tmp = RenderTexture.GetTemporary(1024, 1024, 0, RenderTextureFormat.ARGBFloat);
        Graphics.Blit(rtex, tmp);
        Graphics.Blit(tmp, rtex, drawMaterial);
        RenderTexture.ReleaseTemporary(tmp);
    }

    private void OnGUI() {
        GUI.DrawTexture(new Rect(0, 0, 128, 128), rtex, ScaleMode.ScaleToFit, false, 1);
    }
}
