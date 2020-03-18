using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class DamageContactInteractable : BaseContactInteractables{
    public int damage;
    private EventEmitter ee;

    private void Start(){
        //ee = GameObject.FindGameObjectWithTag("EventEmitter").GetComponent<EventEmitter>();
        ee = FindObjectOfType<EventEmitter>();
    }

    public override void interact(GameObject initiator) {
        Health h = initiator.GetComponent<Health>();
        if (h != null) h.damage(damage);

        ee.invoke("player_damaged", (new[] {this.gameObject}));
    }
}
