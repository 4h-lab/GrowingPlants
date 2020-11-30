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
            //TODO: optimize this
            RaycastHit2D hitSx = Physics2D.Raycast((Vector2)go.transform.position - new Vector2(go.GetComponent<SpriteRenderer>().bounds.extents.x, 0), Vector2.down);
            RaycastHit2D hitDx = Physics2D.Raycast((Vector2)go.transform.position + new Vector2(go.GetComponent<SpriteRenderer>().bounds.extents.x, 0), Vector2.down);
            if (!hitSx.collider.gameObject.GetComponent<TargetManager>() && !hitDx.collider.gameObject.GetComponent<TargetManager>()) return;
            //RaycastHit2D hit = Physics2D.Raycast(go.transform.position, Vector2.down);
            if (!go.GetComponent<TargetActivator>()) {
                go.AddComponent<TargetActivator>();
                go.GetComponent<TargetActivator>().physicalTarget = physicalTarget;
            }
            go.GetComponent<TargetActivator>().add(this);
            
        }
    }

    private void OnCollisionStay2D(Collision2D collision)
    {
        GameObject go = collision.collider.gameObject;
        TargetActivator activator = go.GetComponent<TargetActivator>();

        if (go.tag == "Player")
        {
            if (activator.GetNumberOfObservers() < 1)
            {
                activator.add(this);
            }
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
