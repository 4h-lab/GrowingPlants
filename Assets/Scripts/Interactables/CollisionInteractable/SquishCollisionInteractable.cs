using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class SquishCollisionInteractable : MonoBehaviour
{
    private Vector2 o_scale;
    [Tooltip("time in seconds before the player dies")]
    public float death_time = 1f;
    [Tooltip("factor multiplied every frame")]
    public float squish_amount = 0.1f;
    private float time = 0;
    private EventEmitter ee;
    private IsGrounded GRD;
    private LayerMask passableObjectsLayerMask;
    // Start is called before the first frame update
    void Start()
    {
        o_scale = this.transform.parent.Find("Sprite").localScale;
        ee = GameObject.FindGameObjectWithTag("EventEmitter").GetComponent<EventEmitter>();
        GRD = this.transform.parent.GetComponent<IsGrounded>();
        passableObjectsLayerMask = (1 << LayerMask.NameToLayer("onewayplatform")) | (1 << LayerMask.NameToLayer("plant")) | (1 << LayerMask.NameToLayer("collectible")) | (1 << 2);
    }

    // Update is called once per frame
    void Update()
    {

    }
    private void OnTriggerEnter2D(Collider2D collision)
    {
        if ((((1 << collision.gameObject.layer) & passableObjectsLayerMask) != 0)) return;
        this.transform.parent.gameObject.GetComponent<MovementJoystick>().setSquished(true);
    }
    private void OnTriggerStay2D(Collider2D collision)
    {
        if ((((1 << collision.gameObject.layer) & passableObjectsLayerMask) != 0)) return;
        if (GRD.GetGrounded())
        {
            //var plant = this.transform.parent.parent.GetComponent<NormalPlant>().growthSpeed;
            //this.transform.parent.gameObject.GetComponent<BoxCollider2D>().enabled = false;
            this.transform.parent.Find("Sprite").localScale += new Vector3(Time.deltaTime, -Time.deltaTime) * squish_amount;
            //this.transform.parent.position -= Vector3.up * Time.deltaTime * squish_amount*1/2;
            time += Time.deltaTime;
        }

        if (time >= death_time)
        {
            Health h = this.transform.parent.GetComponent<Health>();
            if (h != null) h.damage(1);
            else { Debug.Log("no health"); }

            ee.invoke("player_damaged", (new[] { this.transform.parent.gameObject }));
            time = 0;
        }
    }

    private void OnTriggerExit2D(Collider2D collision)
    {
        if ((((1 << collision.gameObject.layer) & passableObjectsLayerMask) != 0)) return;
        this.transform.parent.gameObject.GetComponent<MovementJoystick>().setSquished(false);
        StartCoroutine(growBack());

    }

    IEnumerator growBack()
    {
        time = 0;
        Debug.Log(this.transform.parent.Find("Sprite").localScale.y + "   " + o_scale.y);
        
        
        while (this.transform.parent.Find("Sprite").localScale.y < o_scale.y)
        {
            this.transform.parent.Find("Sprite").localScale -= new Vector3(Time.deltaTime, -Time.deltaTime) * squish_amount;
            yield return new WaitForEndOfFrame();
        }
        this.transform.parent.Find("Sprite").localScale = o_scale;
        yield return null;
    }
}
