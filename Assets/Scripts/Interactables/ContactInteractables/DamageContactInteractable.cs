using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class DamageContactInteractable : BaseContactInteractables{
    public int damage;

    public override void interact(GameObject initiator) {
        Health h = initiator.GetComponent<Health>();
        if (h != null) h.damage(damage);
    }
}
