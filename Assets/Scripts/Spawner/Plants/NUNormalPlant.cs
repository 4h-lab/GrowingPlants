using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class NUNormalPlant : BasePlant{
    private int layermask_passables;
    private int layermask_oneway;
    private GameObject stem;
    private GameObject bud;
    public float budTime = 2f;
    private float budTimer = 0f;

    private float finalY;


    void Start()
    {
        layermask_passables = (1 << LayerMask.NameToLayer("plant")) | (1 << LayerMask.NameToLayer("passable")) | (1 << 2);
        layermask_oneway = 1 << LayerMask.NameToLayer("onewayplatform");

        stem = this.transform.GetChild(0).gameObject;
        bud = this.transform.GetChild(1).gameObject;
        stem.transform.localScale -= new Vector3(0f, stem.transform.localScale.y, 0f);
        

        finalY = this.transform.position.y + maxHeigth;
        ray_point = (this.gameObject.GetComponent<SpriteRenderer>().sprite.bounds.extents.y * this.transform.localScale.y) + small_radius;
        ee = GameObject.FindGameObjectWithTag("EventEmitter").GetComponent<EventEmitter>();
        ee.invoke("plant_created", (new[] { this.gameObject }));
    }

    void Update(){
        if (budTimer > budTime){
            bud.SetActive(false);
            budTimer = 0;
        }
        budTimer += Time.deltaTime;

        if (transform.position.y < finalY && !stopped)
        {
            checkObstacles();
            transform.Translate(Vector2.up * Time.deltaTime * growthSpeed * GameManager.customTimeScale);

            stem.transform.localScale += Vector3.up * Time.deltaTime * growthSpeed * GameManager.customTimeScale;
            if (stem.transform.localScale.y > maxHeigth - this.GetComponent<SpriteRenderer>().sprite.bounds.extents.y * 2 * this.transform.localScale.y){
                stem.transform.localScale = new Vector3(stem.transform.localScale.x, maxHeigth - this.GetComponent<SpriteRenderer>().sprite.bounds.extents.y * 2 * this.transform.localScale.y, stem.transform.localScale.z);
            }
        }

        if (transform.position.y > finalY){
            transform.position = new Vector2(transform.position.x, finalY);

        }


    }


    public void checkObstacles()
    {
        RaycastHit2D hit = Physics2D.Raycast(transform.position + new Vector3(0f, ray_point, 0f), Vector2.up, small_radius, ~layermask_passables); // fire a raycast directly down the player
        if (hit.collider != null){
            Debug.Log(hit.collider.gameObject.name);
            this.stopped = true;
        }

        RaycastHit2D[] hits = Physics2D.RaycastAll(transform.position + new Vector3(0f, ray_point, 0f), Vector2.up, 20 * small_radius, layermask_oneway);
        foreach (RaycastHit2D h in hits){

            if (h.collider != null){
                Debug.Log("old:" + maxHeigth);

                Debug.Log("posy " + h.collider.transform.position.y);
                float dim_1w = (h.collider.GetComponentInChildren<SpriteRenderer>().sprite.bounds.extents.y * h.collider.GetComponentInChildren<SpriteRenderer>().gameObject.transform.localScale.y);
                float dim_p = (((GetComponent<SpriteRenderer>().sprite.bounds.extents.y * this.transform.localScale.y)));

                Debug.Log("dim_1w " + dim_1w);
                Debug.Log("dim_p " + dim_p);


                maxHeigth = Mathf.Min(h.collider.transform.position.y, maxHeigth);

                Debug.Log(name + " collided with: " + h.collider.gameObject.name + " new: " + maxHeigth);
            }
        }

    }
    private void OnDrawGizmos()
    {
        Gizmos.color = Color.red;
        //Gizmos.DrawLine(this.transform.position, this.transform.position + new Vector3(0f, radius, 0f));
        Gizmos.DrawLine(transform.position + new Vector3(0f, ray_point, 0f), transform.position + new Vector3(0f, ray_point + 0.01f, 0f));
    }
}
