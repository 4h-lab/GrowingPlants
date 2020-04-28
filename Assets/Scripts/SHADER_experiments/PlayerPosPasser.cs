using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerPosPasser : MonoBehaviour{
    private Material m;
    private Transform playert;
    private float time = 0;
    float speed = 2;
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
        
    }

    private void OnCollisionEnter2D(Collision2D collision)
    {
        m.SetFloat("_PlayerPosX", collision.GetContact(0).point.x);
        m.SetFloat("_PlayerPosY", collision.GetContact(0).point.y);
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
