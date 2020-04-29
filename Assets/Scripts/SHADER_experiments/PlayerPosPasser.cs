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
        updater_m.SetVector("_SpritePos", new Vector4(transform.position.x, transform.position.y, 0, 0));
        updater_m.SetVector("_SpriteScale", new Vector4(GetComponent<SpriteRenderer>().sprite.bounds.extents.x, GetComponent<SpriteRenderer>().sprite.bounds.extents.y, 0, 0));


        m = gameObject.GetComponent<Renderer>().material;
        //m.SetFloatArray("_Points", new float[10]);
        lightmap = new RenderTexture(GetComponent<SpriteRenderer>().sprite.texture.width, GetComponent<SpriteRenderer>().sprite.texture.height, 0, RenderTextureFormat.ARGBFloat);
        m.SetTexture("_ColorMaskTexture", lightmap);
    }
    void Start(){
        playert = GameObject.FindGameObjectWithTag("Player").transform;

        //m.SetTexture("_Colorm", passMap);

    }

    // Update is called once per frame
    void Update(){
        //updater_m.SetVector("_Point", this.GetComponent<SpriteRenderer>().sprite.bounds.center);
        updater_m.SetVector("_Point", new Vector4(playert.position.x, playert.position.y, 0, 0));
        
        RenderTexture temp = RenderTexture.GetTemporary(lightmap.width, lightmap.height, 0, RenderTextureFormat.ARGBFloat);
        Graphics.Blit(lightmap, temp);
        Graphics.Blit(temp, lightmap, updater_m);
        RenderTexture.ReleaseTemporary(temp);
    }

    private void OnCollisionEnter2D(Collision2D collision){
        return;
        updater_m.SetVector("_Point", new Vector4(playert.position.x, playert.position.y, 0,0));
        
        RenderTexture temp = RenderTexture.GetTemporary(lightmap.width, lightmap.height, 0, RenderTextureFormat.ARGBFloat);
        Graphics.Blit(lightmap, temp);
        Graphics.Blit(temp, lightmap, updater_m);
        RenderTexture.ReleaseTemporary(temp);

        //StartCoroutine(colorSprite());

    }

    IEnumerator colorSprite(){
        
        while (time < ray) {
            updater_m.SetVector("_Point", new Vector4(playert.position.x, playert.position.y, 0, 0));

            time += Time.deltaTime*speed;
            m.SetFloat("_Ray", time);
        yield return new WaitForSeconds(.1f);
        }
        time = 0;
        yield break;

    }

    /*
    RenderTexture tmp = RenderTexture.GetTemporary(256, 256, 0, RenderTextureFormat.ARGBFloat);
    Graphics.Blit(passMap, tmp);
    Graphics.Blit(tmp, passMap, drawM);
    RenderTexture.ReleaseTemporary(tmp);
    */
    private void OnGUI(){
        float ratio = GetComponent<SpriteRenderer>().sprite.texture.height / GetComponent<SpriteRenderer>().sprite.texture.width;

        GUI.DrawTexture(new Rect(x, 0, 100, 100*ratio), lightmap, ScaleMode.ScaleToFit, false, 1);
    }
}

    

    

