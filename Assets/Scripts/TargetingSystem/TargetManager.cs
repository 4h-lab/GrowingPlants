using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class TargetManager: MonoBehaviour{
    public GameObject physicalTarget;
    public float teleportDistance = 7f;

    private void Start() {
        if (physicalTarget != null) physicalTarget.GetComponent<Target>().teleportDistance = teleportDistance;
    }

    private void OnCollisionEnter2D(Collision2D collision) {
        GameObject go = collision.collider.gameObject;
        if (go.tag == "Player") {
            RaycastHit2D hit = Physics2D.Raycast(go.transform.position, Vector2.down);
            if (!hit.collider.gameObject.GetComponent<TargetManager>()) return;
            if (!go.GetComponent<TargetActivator>()) {
                go.AddComponent<TargetActivator>();
                go.GetComponent<TargetActivator>().physicalTarget = physicalTarget;
            }
            go.GetComponent<TargetActivator>().add(this);
            
        }
    }

    private void OnCollisionExit2D(Collision2D collision) {
        GameObject go = collision.collider.gameObject;
        if (go.tag == "Player")
        {
            collision.collider.gameObject.GetComponent<TargetActivator>().remove(this);
        }
    }
}
