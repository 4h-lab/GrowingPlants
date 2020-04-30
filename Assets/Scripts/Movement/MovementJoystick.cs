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

    private FixedJoystick variableJoystick;

    private Vector3 targetVelocity = Vector3.zero;

    [SerializeField]
    ContactFilter2D cf;

    private RaycastHit2D[] hitBuffer = new RaycastHit2D[16];

    private void Awake()
    {
        variableJoystick = GameObject.FindObjectOfType<FixedJoystick>();
    }
    void Start() {

        ee = GameObject.FindGameObjectWithTag("EventEmitter").GetComponent<EventEmitter>();
        
        //var lm = Physics2D.GetLayerCollisionMask(gameObject.layer)+LayerMask.NameToLayer("plant");
        //cf.SetLayerMask(lm);
        mBody = this.GetComponent<Rigidbody2D>();
        oldPos = this.transform.position;
        playerCollider = this.GetComponent<BoxCollider2D>();
        mVelocity = Vector3.zero;
        speed = 0f;
    }

    public void FixedUpdate(){
        oldPos = transform.position;
        if (variableJoystick.Horizontal != 0f) {
            Vector3 dir = Vector3.right * Mathf.Sign(variableJoystick.Horizontal);
            movePlayer(dir);
        }
        else if (Input.GetKey(KeyCode.A)) movePlayer(Vector3.left);//changeVelocity(-1f);
        else if (Input.GetKey(KeyCode.D)) movePlayer(Vector3.right);//changeVelocity(1f);
        else {
                    speed = 0;
                }
        //cast the rb and see if it'll be stuck in something


        // update speed accounting for effective movement
        //Debug.Log("Speed : " + speed);
        //Debug.Log("moved : " + (oldPos - transform.position).magnitude / (Time.deltaTime) + " --> from: " + oldPos + " to: " + transform.position );



        speed = Mathf.Min(speed, ((oldPos - transform.position).magnitude / (Time.deltaTime)));

    }
    private void movePlayer(Vector3 dir)
    {
        speed += acceleration * Time.deltaTime;
        if (speed > maxSpeed) speed = maxSpeed;
        targetVelocity = dir;
        transform.Translate( projectRB(dir * speed * Time.deltaTime * GameManager.customTimeScale));
        
        
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

    Vector2 projectRB(Vector2 dir){
        float distance = dir.magnitude ; 
        if (mBody != null) {
            int count = mBody.Cast(dir, cf, hitBuffer, distance + 0.01f);
            for (int i = 0; i < count; i++) {
                Vector2 currentNormal = hitBuffer[i].normal;

                float projection = Vector2.Dot(dir, currentNormal);

                Debug.Log("CN:   " + currentNormal + " * " + dir + " = " + projection);
                if (projection < 0) {
                    Debug.Log("dir: " + dir);
                }
                float modifiedDistance = hitBuffer[i].distance;
                distance = modifiedDistance < distance ? modifiedDistance : distance - 0.01f;
            }
        }
        return dir.normalized * distance;
    }

    public float getTargetVelocityX()
    {
        
        return variableJoystick.Horizontal;
    }
}
