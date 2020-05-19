using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class SquishCollisionInteractable : MonoBehaviour
{
    private Vector2 o_scale;
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
    // Start is called before the first frame update
    void Start()
    {
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

    }
    private void OnTriggerEnter2D(Collider2D collision)
    {
        if ((((1 << collision.gameObject.layer) & passableObjectsLayerMaskWithWater) != 0)) return;
        this.transform.parent.gameObject.GetComponent<MovementJoystick>().setSquished(true);
    }
    private void OnTriggerStay2D(Collider2D collision)
    {
        if ((((1 << collision.gameObject.layer) & passableObjectsLayerMaskWithWater) != 0)) return;
        if (GRD.GetGrounded())
        {
            this.transform.parent.Find("Sprite").localScale += new Vector3(Time.deltaTime, -Time.deltaTime) * squish_amount;
            time += Time.deltaTime;
        }

        if (time >= death_time)
        {
            damaged();
        }
    }

    private void OnTriggerExit2D(Collider2D collision)
    {
        if ((((1 << collision.gameObject.layer) & passableObjectsLayerMask) != 0)) return;

        if(((1 << collision.gameObject.layer)& (1 << LayerMask.NameToLayer("water"))) !=0)
        {
            if (this.transform.position.y < collision.gameObject.transform.position.y)
            {
                damaged();
                this.transform.parent.gameObject.GetComponent<MovementJoystick>().setSquished(false);
                return;
            }
        }
        this.transform.parent.gameObject.GetComponent<MovementJoystick>().setSquished(false);
        StartCoroutine(growBack());

    }

    IEnumerator growBack()
    {
        time = 0;
        Debug.Log(this.transform.parent.Find("Sprite").localScale.y + "   " + o_scale.y);
        
        
        while (this.transform.parent.Find("Sprite").localScale.y < o_scale.y)
        {
            this.transform.parent.Find("Sprite").localScale -= new Vector3(Time.deltaTime, -Time.deltaTime) * de_squish_amount;
            yield return new WaitForEndOfFrame();
        }
        this.transform.parent.Find("Sprite").localScale = o_scale;
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
