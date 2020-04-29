﻿using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerPosPasser : MonoBehaviour{
    private Material m;
    private Transform playert;
    private float time = 0;
    float speed = 10;
    float ray=4;

    public float x = 0;

    [SerializeField]
    private Shader updater;
    private Material updater_m;

    private RenderTexture lightmap;
    // Start is called before the first frame update

    private void Awake()
    {
        updater_m = new Material(updater);
        m = gameObject.GetComponent<Renderer>().material;
        //m.SetFloatArray("_Points", new float[10]);
        lightmap = new RenderTexture(GetComponent<SpriteRenderer>().sprite.texture.width, GetComponent<SpriteRenderer>().sprite.texture.height, 0, RenderTextureFormat.ARGBFloat);
        m.SetTexture("_ColorMaskTexture", lightmap);
    }
    void Start(){
        playert = GameObject.FindGameObjectWithTag("Player").transform;
    }

    // Update is called once per frame
    void Update(){
        //updater_m.SetVector("_Point", this.GetComponent<SpriteRenderer>().sprite.bounds.center);
        
    }

    private void OnCollisionEnter2D(Collision2D collision)
    {
        updater_m.SetVector("_Point", this.GetComponent<SpriteRenderer>().sprite.bounds.center);
        
        RenderTexture temp = RenderTexture.GetTemporary(lightmap.width, lightmap.height, 0, RenderTextureFormat.ARGBFloat);
        Graphics.Blit(lightmap, temp);
        Graphics.Blit(temp, lightmap, updater_m);
        RenderTexture.ReleaseTemporary(temp);

    }

    IEnumerator colorSprite()
    {
        while (time < ray) { 
        time += Time.deltaTime*speed;
        m.SetFloat("_Ray", time);
        yield return new WaitForEndOfFrame();
        }
        time = 0;
        yield return null;

    }

    private void OnGUI()
    {
        GUI.DrawTexture(new Rect(x, 0, 100, 100), lightmap,ScaleMode.ScaleToFit,false,1);
    }
}
