using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerPlatformerController : PhysicsObject{
    public float maxSpeed = 7;
    public float acceleration;

    private float speed;

    private SpriteRenderer spriteRenderer;
    private Animator animator;

    bool facingRight = true;

    // Use this for initialization
    void Awake(){
        spriteRenderer = GetComponent<SpriteRenderer>();
        //animator = GetComponent<Animator>();
    }

    protected override void ComputeVelocity(){
        
        Vector2 movedir = Vector2.zero;

        if (Input.GetKey(KeyCode.D)) movedir =  moveplayer(1);
        else if (Input.GetKey(KeyCode.A)) movedir = moveplayer(-1);
        else if (joystick.Horizontal != 0) movedir =  moveplayer((int)Mathf.Sign(joystick.Horizontal));
        else {
            speed = 0;
        }

        speed = Mathf.Min(speed, velocity.magnitude );
        targetVelocity = movedir;


        if (movedir.x > 0 && !facingRight)
            // ... flip the player.
            Flip();
        // Otherwise if the input is moving the player left and the player is facing right...
        else if (movedir.x < 0 && facingRight)
            // ... flip the player.
            Flip();
    }

    private Vector2 moveplayer(int d) {
        speed += acceleration * Time.deltaTime;
        if (speed > maxSpeed) speed = maxSpeed;

        Vector2 movedir = Vector2.zero;
        movedir.x = d;
        return movedir * speed;

    }

    void Flip()
    {
        // Switch the way the player is labelled as facing.
        facingRight = !facingRight;

        // Multiply the player's x local scale by -1.
        //Vector3 theScale =
        this.transform.localScale = Vector3.Scale(new Vector3(-1, 1, 1), this.transform.localScale);
    }

    public float getTargetVelocityX()
    {
        return targetVelocity.x;
    }
}