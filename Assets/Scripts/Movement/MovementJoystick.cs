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
    private Rigidbody2D mBody;
    private BoxCollider2D playerCollider;
    private  Vector3 mVelocity;
    private Vector3 oldPos;

    public FixedJoystick variableJoystick;

    [SerializeField]
    LayerMask layerMask;

    void Start() {

        ee = GameObject.FindGameObjectWithTag("EventEmitter").GetComponent<EventEmitter>();
        mBody = this.GetComponent<Rigidbody2D>();
        oldPos = this.transform.position;
        playerCollider = this.GetComponent<BoxCollider2D>();
        mVelocity = Vector3.zero;
        speed = 0f;
    }

    public void FixedUpdate(){
        if (variableJoystick.Horizontal != 0f) {
            Vector3 dir = Vector3.right * Mathf.Sign(variableJoystick.Horizontal);
            movePlayer(dir);
        }
        else if (Input.GetKey(KeyCode.A)) movePlayer(Vector3.left);//changeVelocity(-1f);
        else if (Input.GetKey(KeyCode.D)) movePlayer(Vector3.right);//changeVelocity(1f);
        else {
                    speed = 0;
                }
        //FinalCollisionCheck();
    }
    private void movePlayer(Vector3 dir)
    {
        speed += acceleration * Time.deltaTime;
        if (speed > maxSpeed) speed = maxSpeed;
        oldPos = transform.position;
        transform.Translate( dir * speed * Time.deltaTime * GameManager.customTimeScale);
        
        
        if ((dir.x > 0) ^ facingRight) Flip();
        
        
    }
    /*
    private void changeVelocity(float f){
        speed += acceleration * Time.deltaTime;
        if (speed > maxSpeed) speed = maxSpeed;
        Vector3 targetVelocity = new Vector2(dir.x * 10f, mBody.velocity.y *GameManager.customTimeScale);
        mBody.velocity = Vector3.SmoothDamp(mBody.velocity, targetVelocity, ref mVelocity, 0.1f);


        if (f > 0 && !facingRight)
            // ... flip the player.
            Flip();
        // Otherwise if the input is moving the player left and the player is facing right...
        else if (f < 0 && facingRight)
            // ... flip the player.
            Flip();
    }
    */
    void Flip()
    {
        // Switch the way the player is labelled as facing.
        facingRight = !facingRight;

        // Multiply the player's x local scale by -1.
        //Vector3 theScale =
        this.transform.localScale = Vector3.Scale(new Vector3(-1, 1, 1), this.transform.localScale);
    }

    private void FinalCollisionCheck()
    {
        

        // Get bounds of Collider
        var bottomRight = new Vector2(playerCollider.bounds.max.x, playerCollider.bounds.max.y);
        var topLeft = new Vector2(playerCollider.bounds.min.x, playerCollider.bounds.min.y);

        

        // Check if the body's current velocity will result in a collision
        if (Physics2D.OverlapArea(topLeft, bottomRight,layerMask))
        {
            // If so, stop the movement
            transform.position = oldPos;
        }
    }
}
