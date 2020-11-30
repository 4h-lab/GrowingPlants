using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CollisionTrigger : GenericEventTrigger
{
    void Start()
    {
        triggered = false;
    }

    private void OnTriggerEnter2D(Collider2D collision)
    {
        if (triggered && deactivateAfterNotify) return;
        if (CheckIfTriggered(collision)) Notify();
    }

    private void OnTriggerStay2D(Collider2D collision)
    {
        if (CheckIfTriggered(collision)) Notify();
    }

    bool CheckIfTriggered(Collider2D collision)
    {
        if (triggered) return false;
        triggered = collision.GetComponent<Collider2D>().gameObject.tag == "Player";
        return triggered;
    }
}
