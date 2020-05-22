using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class testDissolver : MonoBehaviour{
    private Material m;
    private float dissolve = .7f;

    // Start is called before the first frame update
    void Start(){
        m = gameObject.GetComponent<Renderer>().material;
        
    }

    // Update is called once per frame
    void Update(){
        if (dissolve>= 0) { 
        dissolve -= Time.deltaTime * .5f;
        }
        m.SetFloat("_DissolveAmount", dissolve);


        
    }
}
