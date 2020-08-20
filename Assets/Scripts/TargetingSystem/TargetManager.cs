using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class TargetManager: MonoBehaviour{
    public GameObject physicalTarget;
    private void OnCollisionEnter2D(Collision collision) {
        GameObject go = collision.collider.gameObject;
        if (go.tag == "Player") {
            if (!go.GetComponent<TargetActivator>()) {
                go.AddComponent<TargetActivator>();
                go.GetComponent<TargetActivator>().physicalTarget = physicalTarget;
            }
            go.GetComponent<TargetActivator>().add(this);    
            
        }
    }
    private void OnCollisionExit2D(Collision collision) {
        collision.collider.gameObject.GetComponent<TargetActivator>().remove(this);
    }



}
