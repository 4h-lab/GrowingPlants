using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Target : MonoBehaviour{
    private GameObject player;
    private MovementJoystick mj;
    public float teleportDistance = 5f;

    void Start(){
        

    }
    void Update(){
        if (mj != null) {
            float d = mj.Dir;

            if (d == 0) { // target above player
                this.transform.localPosition = new Vector3(0, teleportDistance, 0);
            } else { // target to the left/right
                this.transform.localPosition = new Vector3(d * teleportDistance, 0, 0);
            }

        }
    }

    public void activate() {
        player = GameObject.FindGameObjectWithTag("Player");
        mj = player.GetComponent<MovementJoystick>();
        this.transform.SetParent(player.transform);
        this.transform.localPosition = Vector3.zero;
    }
}
