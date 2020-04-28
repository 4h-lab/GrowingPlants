using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerPosPasser : MonoBehaviour{
    private Material m;
    private Transform playert;
    private float time = 0;
    float speed = 10;
    float ray=4;
    // Start is called before the first frame update

    private void Awake()
    {
        m = gameObject.GetComponent<Renderer>().material;
        //m.SetFloatArray("_Points", new float[10]);
    }
    void Start(){
        playert = GameObject.FindGameObjectWithTag("Player").transform;
    }

    // Update is called once per frame
    void Update(){
        m.SetFloat("_PlayerPosX", playert.position.x);
        m.SetFloat("_PlayerPosY", playert.position.y);
    }

    private void OnCollisionEnter2D(Collision2D collision)
    {
        return;
        var c = this.transform.position + new Vector3(0f,GetComponent<SpriteRenderer>().sprite.bounds.extents.y,0f);
        m.SetFloat("_PlayerPosX", c.x);
        m.SetFloat("_PlayerPosY", c.y);
        StartCoroutine(colorSprite());
        
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
}
