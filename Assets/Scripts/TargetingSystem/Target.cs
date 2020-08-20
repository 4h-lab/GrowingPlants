using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Target : MonoBehaviour{
    private GameObject player;
    private MovementJoystick mj; 

    void Start(){
        player = GameObject.FindGameObjectWithTag("player");
        mj = player.GetComponent<MovementJoystick>();
    }
    void Update(){
        float d = mj.Dir;
        Debug.Log(d);
    }
}
