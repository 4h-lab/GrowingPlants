using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ConveyorBeltTile : MonoBehaviour
{
    // Start is called before the first frame update
    public float speed = 10.0f;
    public float direction = 1;
    void OnCollisionStay2D(Collision2D collision)
    {
        if (collision.gameObject.tag != "Player")
            return;
        
        float conveyorVelocity = speed * Time.deltaTime;
        Rigidbody2D rigidbody = collision.gameObject.GetComponent<Rigidbody2D>();
        var move= conveyorVelocity * Vector2.right * direction;
        rigidbody.velocity = move;
        Debug.Log(rigidbody.velocity);
    }
}
