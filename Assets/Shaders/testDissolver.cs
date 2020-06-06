using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class testDissolver : MonoBehaviour{
    private Material m;
    private float dissolve = 1f;

    // Start is called before the first frame update
    void Start(){
        m = gameObject.GetComponent<Renderer>().material;
        
    }

    // Update is called once per frame
    void Update(){
        dissolve -= Time.deltaTime * 2.5f;
        m.SetFloat("_DissolveAmount", dissolve);


        
    }
}
