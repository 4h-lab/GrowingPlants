using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class SquishContactInteractable : BaseContactInteractables
{
    public int damage;
    private EventEmitter ee;
    // Start is called before the first frame update
    void Start()
    {
        ee = GameObject.FindGameObjectWithTag("EventEmitter").GetComponent<EventEmitter>();
        ee = FindObjectOfType<EventEmitter>();
    }

    public override void interact(GameObject initiator)
    {
        Health h = this.transform.parent.GetComponent<Health>();
        if (h != null) h.damage(damage);

        ee.invoke("player_damaged", (new[] { this.gameObject }));
    }
}
