using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class SquishContactInteractable : BaseContactInteractables
{
    [SerializeField] private int damage;

    [SerializeField] private GameObject player;

    private int passableObjectsLayerMask = 0;
    private IsGrounded Gr;
    private EventEmitter ee;

    void Start(){
        ee = GameObject.FindGameObjectWithTag("EventEmitter").GetComponent<EventEmitter>();
        ee = FindObjectOfType<EventEmitter>();
        ee.on("win", DeactivateSquishCollision);

        passableObjectsLayerMask = (1 << LayerMask.NameToLayer("onewayplatform")) | (1 << LayerMask.NameToLayer("plant")) | (1 << LayerMask.NameToLayer("collectible")) |(1<<2);

        player = GameObject.Find("Player");
        Gr = player.GetComponent<IsGrounded>();
    }

    public override void interact(GameObject initiator){
        if ((((1 << initiator.layer) & passableObjectsLayerMask) != 0) || !Gr.GetGrounded()) return;

        Health h = this.transform.parent.GetComponent<Health>();
        if (h != null) h.damage(damage);

        ee.invoke("player_damaged", (new[] { this.gameObject }));
    }

    public void DeactivateSquishCollision(Object[] p)
    {
        gameObject.GetComponent<BoxCollider2D>().enabled = false;
    }
}
