using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class NormalPlant : MonoBehaviour{
    public float maxHeigth;
    public float growthSpeed;

    private float initY;

    private EventEmitter ee;
    private bool stopped=false;
    private float radius;

    // Start is called before the first frame update
    void Start(){
        initY = this.transform.position.y;
        radius= this.gameObject.GetComponent<SpriteRenderer>().sprite.bounds.extents.y * this.transform.localScale.y;
        ee = GameObject.FindGameObjectWithTag("EventEmitter").GetComponent<EventEmitter>();
        ee.invoke("plant_created", (new[] { this.gameObject }));
    }

    // Update is called once per frame
    void Update(){

        //checkObstacles();
        if (transform.position.y < initY + maxHeigth && !stopped){
            transform.Translate(Vector2.up * Time.deltaTime * growthSpeed * GameManager.customTimeScale);
        }


    }

    public void checkObstacles()
    {
        RaycastHit2D hit = Physics2D.Raycast(transform.position, Vector2.up, radius); // fire a raycast directly down the player



        if (hit.collider != null && hit.collider.tag!="Player")
        {
           
           this.stopped = true;
           Debug.Log("stopped");
        }

        

    }
    private void OnDrawGizmos()
    {
        Gizmos.color = Color.red;

        //Gizmos.DrawLine(this.transform.position, this.transform.position + new Vector3(0f, radius, 0f));
        Gizmos.DrawWireSphere(this.transform.position, radius);
    }
}
