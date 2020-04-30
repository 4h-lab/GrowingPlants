using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerPosPasser : MonoBehaviour{
    private Material m;
    private Transform playert;
    private float time = 0;
    float speed = 10;
    float ray=4;
    [SerializeField]
    
    public float x = 0;

    [SerializeField]
    private Shader updater;
    private Material updater_m;

    private RenderTexture lightmap;
    // Start is called before the first frame update

    private void Awake()
    {
        updater_m = new Material(updater);
        float cornerX = GetComponent<SpriteRenderer>().sprite.bounds.extents.x;
        float cornerY = GetComponent<SpriteRenderer>().sprite.bounds.extents.y;
        updater_m.SetVector("_SpritePos", new Vector4(transform.position.x - cornerX, transform.position.y - cornerY, 0, 0));
        updater_m.SetVector("_SpriteScale", new Vector4(2*cornerX, 2*cornerY, 0, 0));




        m = gameObject.GetComponent<Renderer>().material;
        //m.SetFloatArray("_Points", new float[10]);
        lightmap = new RenderTexture(GetComponent<SpriteRenderer>().sprite.texture.width, GetComponent<SpriteRenderer>().sprite.texture.height, 0, RenderTextureFormat.ARGBFloat);
        m.SetTexture("_ColorMaskTexture", lightmap);
    }
    void Start(){
        playert = GameObject.FindGameObjectWithTag("Player").transform;

        //m.SetTexture("_Colorm", passMap);
        StartCoroutine(colorSprite());
    }

    // Update is called once per frame
    void Update(){
        Vector4[] arr = new Vector4[10];
        for (int i = 0; i < 10; i++) {
            arr[i] = new Vector4(Random.Range(0,1f), Random.Range(0, 1f), 0, 0);
        }
        updater_m.SetVectorArray("_DaPoints", arr);
        updater_m.SetInt("_DaPointsCount", 10);
        

        //updater_m.SetVector("_Point", this.GetComponent<SpriteRenderer>().sprite.bounds.center);
        updater_m.SetVector("_Point", new Vector4(playert.position.x, playert.position.y, 0, 0));
        updater_m.SetFloat("_Ray", Random.Range(.5f, 3.5f));
        RenderTexture temp = RenderTexture.GetTemporary(lightmap.width, lightmap.height, 0, RenderTextureFormat.ARGBFloat);
        Graphics.Blit(lightmap, temp);
        Graphics.Blit(temp, lightmap, updater_m);
        RenderTexture.ReleaseTemporary(temp);
    }
    

    IEnumerator colorSprite(){
        updater_m.SetVector("_Point", new Vector4(playert.position.x, playert.position.y, 0, 0));
        updater_m.SetFloat("_Ray", Random.Range(.5f, 3.5f));
        RenderTexture temp = RenderTexture.GetTemporary(lightmap.width, lightmap.height, 0, RenderTextureFormat.ARGBFloat);
        Graphics.Blit(lightmap, temp);
        Graphics.Blit(temp, lightmap, updater_m);
        RenderTexture.ReleaseTemporary(temp);
        yield return new WaitForSeconds(.1f);
    }

    private void OnGUI(){
        float ratio = GetComponent<SpriteRenderer>().sprite.texture.height / GetComponent<SpriteRenderer>().sprite.texture.width;

        GUI.DrawTexture(new Rect(x, 0, 100, 100*ratio), lightmap, ScaleMode.ScaleToFit, false, 1);
    }
}

    

    

