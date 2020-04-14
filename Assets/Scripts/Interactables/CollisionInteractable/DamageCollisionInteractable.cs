using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class DamageCollisionInteractable : BaseCollisionInteractable{
    public int damage;
    private EventEmitter ee;

    private void Start(){
        ee = GameObject.FindGameObjectWithTag("EventEmitter").GetComponent<EventEmitter>();
    }

    public override void interact(GameObject initiator) {
        Health h = initiator.GetComponent<Health>();
        if (h != null) h.damage(damage);
        else { Debug.Log("no health"); }

        ee.invoke("player_damaged", (new[] {this.gameObject}));
    }
}
