using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class SquishCollisionInteractable : MonoBehaviour
{
    private Vector2 o_scale;
    private Vector2 box_size;
    private BoxCollider2D collider; 
    [Tooltip("time in seconds before the player dies")]
    public float death_time = 0.5f;
    [Tooltip("factor multiplied every frame to squish the player")]
    public float squish_amount =1f;
    [Tooltip("factor multiplied every frame to go back to normal scale")]
    public float de_squish_amount = 2f;
    private float time = 0;
    private EventEmitter ee;
    private IsGrounded GRD;
    private LayerMask passableObjectsLayerMask;
    private LayerMask passableObjectsLayerMaskWithWater;

    private Transform sprite;

    private int collisionCounter=0;
    // Start is called before the first frame update
    void Start()
    {

        sprite = this.transform.parent.Find("Sprite");
        collider = this.transform.parent.gameObject.GetComponent<BoxCollider2D>();
        box_size = collider.size;
        o_scale = this.transform.parent.Find("Sprite").localScale;
        ee = GameObject.FindGameObjectWithTag("EventEmitter").GetComponent<EventEmitter>();
        GRD = this.transform.parent.GetComponent<IsGrounded>();
        passableObjectsLayerMask = (1 << LayerMask.NameToLayer("onewayplatform")) | (1 << LayerMask.NameToLayer("plant")) | (1 << LayerMask.NameToLayer("collectible")) | (1 << 2);
        passableObjectsLayerMaskWithWater = (1 << LayerMask.NameToLayer("onewayplatform")) | (1 << LayerMask.NameToLayer("plant")) | (1 << LayerMask.NameToLayer("collectible")) | (1 << 2) | (1 << LayerMask.NameToLayer("water"));

        ee.on("win", DeactivateSquishCollision);
    }

    // Update is called once per frame
    void Update()
    {
        if (GRD.GetGrounded() && collisionCounter > 0)
        {
            sprite.localScale += new Vector3(Time.deltaTime * 2f, -Time.deltaTime) * squish_amount * GameManager.FindObjectOfType<GameManager>().GetCustomTimeScale();
            collider.size += new Vector2(0f, -Time.deltaTime*2) * squish_amount * GameManager.FindObjectOfType<GameManager>().GetCustomTimeScale();
            time += Time.deltaTime;
        }

        if (time >= death_time)
        {
            damaged();
        }
    }
    private void OnTriggerEnter2D(Collider2D collision){
        PopupText.createNewPopup(transform.position, "AHHHHHH!!!!!!!!", Color.white, 10f, 2f, PopupText.utilFuncs_moveUp);

        // if its colliding with the water......
        if (((1 << collision.gameObject.layer) & (1 << LayerMask.NameToLayer("water"))) != 0) {
            damaged(); // kill the player
            this.transform.parent.gameObject.GetComponent<MovementJoystick>().setSquished(false);
            return;
        }

        // if the layer is passable... just ignore it
        if ((((1 << collision.gameObject.layer) & passableObjectsLayerMaskWithWater) != 0)) return;
        this.transform.parent.gameObject.GetComponent<MovementJoystick>().setSquished(true); // otherwise start squishing
        
        collisionCounter++;
        Debug.Log("COLLISIONI:" + collisionCounter);
        

        


    }
    /*private void OnTriggerStay2D(Collider2D collision)
    {
        if ((((1 << collision.gameObject.layer) & passableObjectsLayerMaskWithWater) != 0)) return;
        if (GRD.GetGrounded() && collisionCounter > 0)
        {
            sprite.localScale += new Vector3(Time.deltaTime*2f, -Time.deltaTime) * squish_amount/collisionCounter*GameManager.FindObjectOfType<GameManager>().GetCustomTimeScale();
            time += Time.deltaTime/collisionCounter;
            Debug.Log(Time.deltaTime / collisionCounter);
        }

        if (time >= death_time)
        {
            damaged();
        }
    }*/

    

    private void OnTriggerExit2D(Collider2D collision){
        if ((((1 << collision.gameObject.layer) & passableObjectsLayerMask) != 0)) return;

        if(((1 << collision.gameObject.layer)& (1 << LayerMask.NameToLayer("water"))) !=0) 
        {
            if (this.transform.position.y < collision.gameObject.transform.position.y) ///If the player is belowe water level kill it
            {
                damaged();
                this.transform.parent.gameObject.GetComponent<MovementJoystick>().setSquished(false);
                return;
            }
        }
        collisionCounter--;
        Debug.Log("COLLISIONI:" + collisionCounter);
        if (collisionCounter == 0) {
            this.transform.parent.gameObject.GetComponent<MovementJoystick>().setSquished(false);
            StartCoroutine(growBack());
        }
        

    }

    IEnumerator growBack()
    {
        time = 0;
        this.transform.parent.gameObject.GetComponent<Collider2D>().isTrigger = false;
        this.transform.parent.gameObject.GetComponent<Rigidbody2D>().isKinematic = false;
        Debug.Log(sprite.localScale.y + "   " + o_scale.y);

        
        collider.size = box_size;
        while (sprite.localScale.y < o_scale.y)
        {
            sprite.localScale -= new Vector3(Time.deltaTime*2f, -Time.deltaTime) * de_squish_amount;
            
            yield return new WaitForEndOfFrame();
        }

        sprite.localScale = o_scale;
        
        yield return null;
    }

    public void DeactivateSquishCollision(Object[] p)
    {
        gameObject.GetComponent<BoxCollider2D>().enabled = false;
    }

    private void damaged()
    {
        Health h = this.transform.parent.GetComponent<Health>();
        if (h != null) h.damage(1);
        else { Debug.Log("no health"); }

        ee.invoke("player_damaged", (new[] { this.transform.parent.gameObject }));
        time = 0;
    }
}
