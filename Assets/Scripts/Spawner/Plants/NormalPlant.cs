﻿using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class NormalPlant : MonoBehaviour{
    public float maxHeigth;
    public float growthSpeed;
    
    // Start is called before the first frame update
    void Start(){
        
    }

    // Update is called once per frame
    void Update(){
        transform.Translate(Vector2.up * Time.deltaTime * growthSpeed );
    }
}
