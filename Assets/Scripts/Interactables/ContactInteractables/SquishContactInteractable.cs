using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class SquishContactInteractable : BaseContactInteractables
{
    public int damage;
    private EventEmitter ee;

    private int passableObjectsLayerMask = 0;

    void Start(){
        ee = GameObject.FindGameObjectWithTag("EventEmitter").GetComponent<EventEmitter>();
        ee = FindObjectOfType<EventEmitter>();
        ee.on("win", DeactivateSquishCollision);

        passableObjectsLayerMask = (1 << LayerMask.NameToLayer("onewayplatform")) | (1 << LayerMask.NameToLayer("plant")) | (1 << LayerMask.NameToLayer("collectible"));
    }

    public override void interact(GameObject initiator){
        if (((1 << initiator.layer) & passableObjectsLayerMask) != 0) return;

        Health h = this.transform.parent.GetComponent<Health>();
        if (h != null) h.damage(damage);

        ee.invoke("player_damaged", (new[] { this.gameObject }));
    }

    public void DeactivateSquishCollision(Object[] p)
    {
        gameObject.GetComponent<BoxCollider2D>().enabled = false;
    }
}
