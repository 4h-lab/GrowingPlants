using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class TestPlantNotifier : MonoBehaviour{
    private EventEmitter ee;
    private int numofplants = 0;

    private void newplant(Object[] o) {
        numofplants++;
    }

    // Start is called before the first frame update
    void Start(){
        ee = GameObject.FindGameObjectWithTag("EventEmitter").GetComponent<EventEmitter>();
        ee.on("plant_created", newplant);
    }

}
