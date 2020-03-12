using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class NormalPlant : MonoBehaviour{
    public float maxHeigth;
    public float growthSpeed;

    private float initY;

    // Start is called before the first frame update
    void Start(){
        initY = this.transform.position.y;
    }

    // Update is called once per frame
    void Update(){
        if (transform.position.y < initY + maxHeigth){
            transform.Translate(Vector2.up * Time.deltaTime * growthSpeed);
        }

    }
        
}
