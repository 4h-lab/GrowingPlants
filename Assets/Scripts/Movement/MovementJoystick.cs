using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class MovementJoystick : MonoBehaviour{
    public float maxSpeed;
    public float acceleration;


    private EventEmitter ee;
    private float speed;
    bool facingRight = true;
    private Rigidbody2D mBody;
    private BoxCollider2D playerCollider;
    private  Vector3 mVelocity;
    private Vector3 oldPos;
    private bool isSquished = false;
    private bool isRunning = false;

    //TODO: refactor
    private bool isJumping = false;


    // animations
    private Animator anim;
    private ParticleSystem dust;

    private FixedJoystick variableJoystick;

    private Vector3 targetVelocity = Vector3.zero;

    [SerializeField]
    ContactFilter2D cf;
    [SerializeField]
    [Tooltip("if < 0, turns off falling speed limiting function")]
    private float maxFallingSpeed = 10;
    private float standardGravityScale;

    private RaycastHit2D[] hitBuffer = new RaycastHit2D[16];
    private IsGrounded grounded;

    private Vector3 lastDirection;

    private void Awake()
    {
        variableJoystick = GameObject.FindObjectOfType<FixedJoystick>();
    }
    void Start() {
        anim = GetComponentInChildren<Animator>();
        dust = GameObject.FindGameObjectWithTag("playerps_dustrunning").GetComponent<ParticleSystem>();

        ee = GameObject.FindGameObjectWithTag("EventEmitter").GetComponent<EventEmitter>();
        
        //var lm = Physics2D.GetLayerCollisionMask(gameObject.layer)+LayerMask.NameToLayer("plant");
        //cf.SetLayerMask(lm);
        mBody = this.GetComponent<Rigidbody2D>();
        standardGravityScale = mBody.gravityScale;
        oldPos = this.transform.position;
        playerCollider = this.GetComponent<BoxCollider2D>();
        mVelocity = Vector3.zero;
        speed = 0f;
        grounded = GetComponent<IsGrounded>();
    }

    private void Update()
    {
        if (Input.GetKeyDown(KeyCode.A)) lastDirection = Vector3.left;
        if (Input.GetKeyDown(KeyCode.D)) lastDirection = Vector3.right;
    }

    public void FixedUpdate(){
        isRunning = false;
        oldPos = transform.position;
        if (variableJoystick.Horizontal != 0f)
        {
            Vector3 dir = Vector3.right * Mathf.Sign(variableJoystick.Horizontal);
            movePlayer(dir);
        }
        else if (Input.GetKey(KeyCode.A) && Input.GetKey(KeyCode.D)) movePlayer(lastDirection);
        else if (Input.GetKey(KeyCode.A)) movePlayer(Vector3.left);
        else if (Input.GetKey(KeyCode.D)) movePlayer(Vector3.right);
        else speed = 0;
        //cast the rb and see if it'll be stuck in something
        // update speed accounting for effective movement
        //Debug.Log("Speed : " + speed);
        //Debug.Log("moved : " + (oldPos - transform.position).magnitude / (Time.deltaTime) + " --> from: " + oldPos + " to: " + transform.position );

        if (maxFallingSpeed >= 0) AlterFallingSpeed();
        speed = Mathf.Min(speed, ((oldPos - transform.position).magnitude / (Time.deltaTime)));

        if (speed > 0) isRunning = true;
        if (anim != null) anim.SetBool("running", isRunning);

        if (dust != null) {
            if (isRunning && grounded.GetGrounded()) {
                if(!dust.isPlaying) dust.Play();

            } else {
                if(!dust.isStopped)dust.Stop();
            } 
        }


        //TODO: this is probably gonna break something
        if (mBody.velocity.y <= 0) isJumping = false;
        if (anim != null) anim.SetBool("jumping", isJumping);


    }
    private void movePlayer(Vector3 dir){
        ee.invoke("playermoved", null);


        speed += acceleration * Time.deltaTime;
        if (speed > maxSpeed) speed = maxSpeed;
        targetVelocity = dir;
        if (!isSquished){

            transform.Translate(projectRB(dir * speed * Time.deltaTime * GameManager.customTimeScale));
        }
        else
        {
            transform.Translate(dir * speed * Time.deltaTime * GameManager.customTimeScale);
        }
        if ((dir.x > 0) ^ facingRight) Flip();
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
        //this.transform.localScale = Vector3.Scale(new Vector3(-1, 1, 1), this.transform.localScale);
        //this.transform.Find("Sprite").GetComponent<SpriteRenderer>().flipX = !this.transform.Find("Sprite").GetComponent<SpriteRenderer>().flipX;
        Transform t = this.transform.Find("Sprite").transform.Find("Animated").transform;
        t.localScale = new Vector3(-t.localScale.x, t.localScale.y, t.localScale.z);

        if (dust != null) dust.transform.Rotate(new Vector3(0,0,180));


    }

    void AlterFallingSpeed()
    {
        
        if ((mBody.velocity.y < 0) && (mBody.velocity.magnitude > maxFallingSpeed))
        {
            if (mBody.gravityScale != 0) mBody.gravityScale = 0;
        }

        else
        {
            if (mBody.gravityScale != standardGravityScale) mBody.gravityScale = standardGravityScale;
        }
        

    }
     
    Vector2 projectRB(Vector2 dir){
        float distance = dir.magnitude ; 
        if (mBody != null) {
            int count = mBody.Cast(dir, cf, hitBuffer, distance + 0.01f);
            for (int i = 0; i < count; i++) {
                Vector2 currentNormal = hitBuffer[i].normal;

                float projection = Vector2.Dot(dir, currentNormal);

                float modifiedDistance = hitBuffer[i].distance;
                distance = modifiedDistance < distance ? modifiedDistance : distance - 0.01f;
            }
        }

        return dir.normalized * distance;
    }
    public void setSquished(bool s)
    {
        isSquished = s;
    }

    public bool getSquished()
    {
        return isSquished;
    }
    public float getTargetVelocityX()
    {
        
        return variableJoystick.Horizontal;
    }

    public void SetIsJumping(bool isJumping)
    {
        this.isJumping = isJumping;
    }

    public bool IsJumping()
    {
        return isJumping;
    }
}
