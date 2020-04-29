using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class something : MonoBehaviour{
    private Material m;
    private Transform playert;

    private RenderTexture rtex;
    

    // Start is called before the first frame update
    void Start(){
        m = gameObject.GetComponent<Renderer>().material;
        playert = GameObject.FindGameObjectWithTag("Player").transform;

        //rtex = new RenderTexture(1024, 1024, 0, RenderTextureFormat.ARGBFloat);
        rtex =(RenderTexture) m.GetTexture("_MainTex");
        m.SetColor("_ColorTint", Color.blue);
        //m.SetTexture("_MainTex", rtex);
    }

    // Update is called once per frame
    void Update(){
        m.SetFloat("_PlayerPosX", playert.position.x);
        m.SetFloat("_PlayerPosY", playert.position.y);
        
        /*
        RenderTexture tmp = RenderTexture.GetTemporary(256, 256, 0, RenderTextureFormat.ARGBFloat);
        Graphics.Blit(rtex, tmp);
        Graphics.Blit(tmp, rtex, m);
        RenderTexture.ReleaseTemporary(tmp);
       */

    }
}
