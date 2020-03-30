using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class MovementJoystick : MonoBehaviour
{
    private EventEmitter ee;

    public float maxSpeed;
    public float acceleration;
    private float speed;
    bool facingRight = true;

    public FixedJoystick variableJoystick;

    void Start() {
        ee = GameObject.FindGameObjectWithTag("EventEmitter").GetComponent<EventEmitter>();
        speed = 0f;
    }

    public void FixedUpdate(){
        if (variableJoystick.Horizontal != 0f) {
            Vector3 dir = Vector3.right * Mathf.Sign(variableJoystick.Horizontal);
            changeVelocity(variableJoystick.Horizontal);
        }
        else if (Input.GetKey(KeyCode.A)) changeVelocity(-1f);
        else if (Input.GetKey(KeyCode.D)) changeVelocity(1f);
        else {
            speed = 0;
            changeVelocity(0f);
        }
    }
    private void movePlayer(Vector3 dir) {
        speed += acceleration * Time.deltaTime;
        if (speed > maxSpeed) speed = maxSpeed;

        transform.position += dir * speed * Time.deltaTime * GameManager.customTimeScale;
    
    }
    private void changeVelocity(float f)
    {
        GetComponent<Rigidbody2D>().velocity = new Vector2(f * maxSpeed, GetComponent<Rigidbody2D>().velocity.y);

        if (f > 0 && !facingRight)
            // ... flip the player.
            Flip();
        // Otherwise if the input is moving the player left and the player is facing right...
        else if (f < 0 && facingRight)
            // ... flip the player.
            Flip();
    }

    void Flip()
    {
        // Switch the way the player is labelled as facing.
        facingRight = !facingRight;

        // Multiply the player's x local scale by -1.
        Vector3 theScale = this.transform.localScale;
        theScale.x *= -1;
        this.transform.localScale = theScale;
    }

}
