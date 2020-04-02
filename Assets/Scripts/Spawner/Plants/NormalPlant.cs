using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class NormalPlant : BasePlant{
    private int layermask_passables;
    private int layermask_oneway;

    void Start(){
        layermask_passables = (1 << LayerMask.NameToLayer("plant")) | (1 << LayerMask.NameToLayer("passable")) | (1 << 2);
        layermask_oneway = 1 << LayerMask.NameToLayer("onewayplatform");


        initY = this.transform.position.y;
        ray_point= (this.gameObject.GetComponent<SpriteRenderer>().sprite.bounds.extents.y * this.transform.localScale.y)+small_radius;
        ee = GameObject.FindGameObjectWithTag("EventEmitter").GetComponent<EventEmitter>();
        ee.invoke("plant_created", (new[] { this.gameObject }));
    }

    void Update(){
        if (transform.position.y < initY + maxHeigth && !stopped){
            checkObstacles();
            transform.Translate(Vector2.up * Time.deltaTime * growthSpeed * GameManager.customTimeScale);
        }

        if (transform.position.y > initY + maxHeigth) transform.position = new Vector2(transform.position.x, initY + maxHeigth);

    }

    public void checkObstacles(){
        RaycastHit2D hit = Physics2D.Raycast(transform.position+new Vector3(0f,ray_point,0f), Vector2.up,small_radius, ~layermask_passables); // fire a raycast directly down the player
        //if (hit.collider != null && hit.collider.tag!="Player" && hit.collider.GetComponent<Passable>()==null){
        if (hit.collider != null) {
           Debug.Log(hit.collider.gameObject.name);
           this.stopped = true;
        }

        RaycastHit2D[] hits = Physics2D.RaycastAll(transform.position + new Vector3(0f, ray_point, 0f), Vector2.up, 20*small_radius, layermask_oneway);
        foreach ( RaycastHit2D h in hits) {
            if (h.collider != null){
                Debug.Log( name + " collided with: " +  h.collider.gameObject.name);
                maxHeigth = Mathf.Min((h.collider.transform.position.y - initY), maxHeigth);
            }
        }
        
    }
    private void OnDrawGizmos(){
        Gizmos.color = Color.red;
        //Gizmos.DrawLine(this.transform.position, this.transform.position + new Vector3(0f, radius, 0f));
        Gizmos.DrawLine(transform.position + new Vector3(0f, ray_point, 0f), transform.position + new Vector3(0f, ray_point+0.01f, 0f));
    }
}
