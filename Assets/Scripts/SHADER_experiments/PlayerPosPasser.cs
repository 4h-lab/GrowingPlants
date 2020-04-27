using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerPosPasser : MonoBehaviour{
    private Material m;
    private Transform playert;

    // Start is called before the first frame update
    void Start(){
        m = gameObject.GetComponent<Renderer>().material;
        playert = GameObject.FindGameObjectWithTag("Player").transform;
    }

    // Update is called once per frame
    void Update(){
        m.SetFloat("_PlayerPosX", playert.position.x);
        m.SetFloat("_PlayerPosY", playert.position.y);
    }
}
