using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerPosPasser : MonoBehaviour{
    public Shader drawShader;
    private RenderTexture passMap; //this represent sort of an "heigthmap" that contains informations about the player passage.; basically is a texture in which every pixel color represent how near the player passed from a specific point


    private Material drawM;
    private Material m;
    private Transform playert;

    private float time = 0;
    float speed = 10;
    float ray=4;

    // Start is called before the first frame update
    private void Awake(){
        drawM = new Material(drawShader);
        drawM.SetColor("_ColorTint", Color.red); 
        m = gameObject.GetComponent<Renderer>().material;
    }

    void Start(){
        playert = GameObject.FindGameObjectWithTag("Player").transform;
        passMap = new RenderTexture(256, 256, 0, RenderTextureFormat.ARGBFloat);
        m.SetTexture("_MainTex", passMap);
    }

    // Update is called once per frame
    void Update(){
        drawM.SetFloat("_PlayerPosX", playert.position.x);
        drawM.SetFloat("_PlayerPosY", playert.position.y);

        m.SetFloat("_PlayerPosX", playert.position.x);
        m.SetFloat("_PlayerPosY", playert.position.y);


        /*
        RenderTexture tmp = RenderTexture.GetTemporary(256, 256, 0, RenderTextureFormat.ARGBFloat);
        Graphics.Blit(passMap, tmp);
        Graphics.Blit(tmp, passMap, drawM);
        RenderTexture.ReleaseTemporary(tmp);
        */
    }

    private void OnGUI() {
        GUI.DrawTexture(new Rect(0, 0, 128, 128), passMap, ScaleMode.ScaleToFit, false, 1);
    }
}
