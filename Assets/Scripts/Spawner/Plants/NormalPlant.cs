using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class NormalPlant : MonoBehaviour{
    public float maxHeigth;
    public float growthSpeed;

    private float initY;

    private EventEmitter ee;

    // Start is called before the first frame update
    void Start(){
        initY = this.transform.position.y;
        ee = GameObject.FindGameObjectWithTag("EventEmitter").GetComponent<EventEmitter>();
        ee.invoke("plant_created", (new[] { this.gameObject }));
    }

    // Update is called once per frame
    void Update(){
        if (transform.position.y < initY + maxHeigth){
            transform.Translate(Vector2.up * Time.deltaTime * growthSpeed);
        }

    }
        
}
