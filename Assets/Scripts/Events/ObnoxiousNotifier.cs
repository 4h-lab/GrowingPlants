using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ObnoxiousNotifier : MonoBehaviour{

    private EventEmitter ee;

    private void notifyPlayerDamage(Object[] p) {
        string msg = "Player damaged ";
        if (p != null) msg += ((GameObject)p[0]).name;
        Debug.Log(msg);
    }
    private void notifyPlantCreated(Object[] p) {
        string msg = "Plant created ";
        if (p != null) msg += ((GameObject)p[0]).name;
        Debug.Log(msg);
    }

    private void notifyPlayerSquished(Object[] p)
    {
        string msg = "Player has squished ";
        if (p != null) msg += ((GameObject)p[0]).name;
        Debug.Log(msg);
    }

    // Start is called before the first frame update
    void Start(){
        ee = GameObject.FindGameObjectWithTag("EventEmitter").GetComponent<EventEmitter>();
        ee.on("player_damaged", notifyPlayerDamage);
        ee.on("plant_created", notifyPlantCreated);
        ee.on("player_squished", notifyPlayerSquished);
    }

}
