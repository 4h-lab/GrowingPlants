using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Water : MonoBehaviour {
    public float risingSpeed;
    private bool cangoup = false;
    private float initY;
    private EventEmitter ee;


    private void triggerRisingLevel(Object[] p){
        cangoup = true;
    }

    private void stopRisingLevel(Object[] p)
    {
        cangoup = false;
    }

    // Start is called before the first frame update
    void Start(){
        initY = this.transform.position.y;
        ee = GameObject.FindGameObjectWithTag("EventEmitter").GetComponent<EventEmitter>();
        ee.on("plant_created", triggerRisingLevel);
        ee.on("win", stopRisingLevel);

    }

    // Update is called once per frame
    void Update(){
        if (cangoup) {
            transform.Translate(Vector2.up * Time.deltaTime * risingSpeed);
        }
    }
}

