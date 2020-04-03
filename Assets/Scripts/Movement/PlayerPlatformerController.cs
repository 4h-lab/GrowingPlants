using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerPlatformerController : PhysicsObject{
    public float maxSpeed = 7;
    public float acceleration;

    private float speed;

    private SpriteRenderer spriteRenderer;
    private Animator animator;
 
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

        
        // todo: flip code
        /*
        bool flipSprite = (spriteRenderer.flipX ? (move.x > 0.01f) : (move.x < 0.01f));
        if (flipSprite && move.x!=0){
            spriteRenderer.flipX = !spriteRenderer.flipX;
        }

        //animator.SetBool("grounded", grounded);
        //animator.SetFloat("velocityX", Mathf.Abs(velocity.x) / maxSpeed);

        targetVelocity = move * maxSpeed;
        */
    }

    private Vector2 moveplayer(int d) {
        speed += acceleration * Time.deltaTime;
        if (speed > maxSpeed) speed = maxSpeed;

        Vector2 movedir = Vector2.zero;
        movedir.x = d;
        return movedir * speed;

    }
}