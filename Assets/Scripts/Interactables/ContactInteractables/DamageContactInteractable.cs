using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class DamageContactInteractable : BaseContactInteractables{
    public int damage;
    public override void interact(GameObject initiator){
        Health health = initiator.GetComponent<Health>();
        if (health != null) {
            health.damage(damage);
        }

    }
}
