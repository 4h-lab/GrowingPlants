using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ConveyorBeltCollisionInteractable : MonoBehaviour, IInteractableWithEvents
{
    public float speed = 100.0f;
    public float direction = 1;
    private EventEmitter ee;

    void Start()
    {
        ee = gameObject.AddComponent(typeof(EventEmitter)) as EventEmitter;
    }

    void OnCollisionStay2D(Collision2D collision)
    {
        
        if (collision.gameObject.tag != "Player")
            return;

        float conveyorVelocity = speed * Time.deltaTime;
        Rigidbody2D rigidbody = collision.gameObject.GetComponent<Rigidbody2D>();
        var move = conveyorVelocity * Vector2.right * direction;
        rigidbody.velocity = move;
    }

   /* void OnTriggerExit2D(Collider2D collision)
    {
        Debug.Log("exiiiiiiiiit");
        if (collision.gameObject.layer == LayerMask.NameToLayer("plant"))
        {
            ee.invoke("plant.falling", new Object[] { this });
        }
    }*/

    public float getSpeed()
    {
        return (speed * direction)/2;
    }
    public void subsribeEvent(string eventname, EventEmitter.EventCallback ev)
    {
        ee.on(eventname, ev);
    }


}
