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
        o_scale = this.transform.localScale;
        ee = GameObject.FindGameObjectWithTag("EventEmitter").GetComponent<EventEmitter>();
        GRD = this.GetComponent<IsGrounded>();
        passableObjectsLayerMask = (1 << LayerMask.NameToLayer("onewayplatform")) | (1 << LayerMask.NameToLayer("plant")) | (1 << LayerMask.NameToLayer("collectible")) | (1 << 2);
    }

    // Update is called once per frame
    void Update()
    {
        
    }

    private void OnCollisionStay2D(Collision2D collision)
    {
        if ((((1 << collision.gameObject.layer) & passableObjectsLayerMask) != 0)) return;
        if (collision.transform.position.y > this.transform.position.y && GRD.GetGrounded())
        {
            this.transform.localScale += new Vector3(Time.deltaTime,  -Time.deltaTime)* squish_amount ;
            this.transform.position -= Vector3.up * Time.deltaTime * squish_amount*1/2;
            time += Time.deltaTime;
        }
        
        if (time >= death_time)
        {
            Health h = this.GetComponent<Health>();
            if (h != null) h.damage(1);
            else { Debug.Log("no health"); }

            ee.invoke("player_damaged", (new[] { this.gameObject }));
            time = 0;
        }
    }

    private void OnCollisionExit2D(Collision2D collision)
    {
    if (collision.transform.position.y > this.transform.position.y)
    {
        StartCoroutine(growBack());
        }
    }

    IEnumerator growBack()
    {
        while(this.transform.localScale.y < o_scale.y)
        {
            this.transform.localScale -= new Vector3(Time.deltaTime, -Time.deltaTime*2)* squish_amount;
        }
        this.transform.localScale = o_scale;
        yield return null;
    }

    public void ScaleAround()
    {

        // finally, actually perform the scale/translation
        this.transform.localScale += new Vector3(Time.deltaTime, -Time.deltaTime) * squish_amount;
        
    }
}
