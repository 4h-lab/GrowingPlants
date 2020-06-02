using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerPosPasser : MonoBehaviour{
    private Material m;
    private Transform playert;
    private float time = 0;
    float speed = 10;
    float ray=4;
    float maxDistToUpdate;
    float maxDistTolerance = 1;
    [SerializeField]
    
    public float x = 0;

    [SerializeField]
    private Shader updater;

    [Tooltip("the amount of color to add every frame, is only the tones of red, any other color part will have no effect")]
    [SerializeField]
    private Color RedIntensity = new Color(0.2f, 0f, 0f);

    private Material updater_m;

    private RenderTexture lightmap;
    private static bool col =true;
    
    private void Awake()
    {
        
        updater_m = new Material(updater);
        float cornerX = GetComponent<SpriteRenderer>().sprite.bounds.extents.x*this.transform.localScale.x;
        float cornerY = GetComponent<SpriteRenderer>().sprite.bounds.extents.y * this.transform.localScale.y;
        updater_m.SetVector("_SpritePos", new Vector4(transform.position.x - cornerX, transform.position.y - cornerY, 0, 0));
        updater_m.SetVector("_SpriteScale", new Vector4(2*cornerX, 2*cornerY, 0, 0));


        m = gameObject.GetComponent<Renderer>().material;

        lightmap = new RenderTexture(32, 32, 0, RenderTextureFormat.ARGBFloat);
        m.SetTexture("_ColorMaskTexture", lightmap);
    }
    void Start(){
        playert = GameObject.FindGameObjectWithTag("Player").transform;

        StartCoroutine(spray());
        maxDistToUpdate = GameObject.FindGameObjectWithTag("Player").GetComponent<PlayerPosUpdater>().baseThrownRadius + 
                            GameObject.FindGameObjectWithTag("Player").GetComponent<PlayerPosUpdater>().baseThrownRadius +
                            GetComponent<SpriteRenderer>().sprite.bounds.extents.y * this.transform.localScale.y +
                            GetComponent<SpriteRenderer>().sprite.bounds.extents.x * this.transform.localScale.x +
                            maxDistTolerance;
    }
    
    IEnumerator spray() {
        while (true) {
            if (Vector2.Distance(transform.position, playert.position) < maxDistToUpdate) {
                RenderTexture temp = RenderTexture.GetTemporary(lightmap.width, lightmap.height, 0, RenderTextureFormat.ARGBFloat);
                Graphics.Blit(lightmap, temp);
                Graphics.Blit(temp, lightmap, updater_m);
                RenderTexture.ReleaseTemporary(temp);
            }
            yield return new WaitForSeconds(.1f);
        }
    }

    /*
    private void OnGUI(){
        float ratio = GetComponent<SpriteRenderer>().sprite.texture.height / GetComponent<SpriteRenderer>().sprite.texture.width;

        GUI.DrawTexture(new Rect(x, 0, 100, 100*ratio), lightmap, ScaleMode.ScaleToFit, false, 1);
    }
    */
    private void OnDestroy()
    {
        lightmap.Release();
    }
}

    

    

