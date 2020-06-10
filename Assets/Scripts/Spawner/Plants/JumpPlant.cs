using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class JumpPlant : BasePlant
{
    [SerializeField] private float budTime = 2f;
    [SerializeField] private float upArc = 45.0f;//the maximum angle there can be between Vector2.down and the normal of a collision between the plant and a body for it to cunt as standing on the plant
    [SerializeField] private Vector2 jumpForce;

    private bool apex;
    private bool playerFound;
    private bool playerFoundAfter;
    private int layermask_passables;
    private int layermask_oneway;
    private float budTimer = 0f;
    private GameObject stem;
    private GameObject bud;

    void Start()
    {
        layermask_passables = (1 << LayerMask.NameToLayer("plant")) | (1 << LayerMask.NameToLayer("passable")) | (1 << 2) | 1 << LayerMask.NameToLayer("onewayplatform") | (1 << LayerMask.NameToLayer("collectible")) | (1 << LayerMask.NameToLayer("water"));
        layermask_oneway = 1 << LayerMask.NameToLayer("onewayplatform");

        stem = this.transform.GetChild(0).gameObject;
        bud = this.transform.GetChild(1).gameObject;
        stem.transform.localScale -= new Vector3(0f, stem.transform.localScale.y, 0f);
        initY = this.transform.position.y;
        ray_point = (this.gameObject.GetComponent<BoxCollider2D>().bounds.extents.y + small_radius);
        ee = GameObject.FindGameObjectWithTag("EventEmitter").GetComponent<EventEmitter>();
        ee.invoke("plant_created", (new[] { this.gameObject }));

        GameObject.FindGameObjectWithTag("GameManager").GetComponent<GameManager>().notifyOfNewSomething("plant.planted");
        apex = false;
        playerFound = false;
        playerFoundAfter = false;
        player = null;
    }

    void Update()
    {
        if (playerFound && !playerFoundAfter)
        {
            apex = false;
            playerFoundAfter = true;
        }

        if (player && stopped && !apex){
            player.GetComponent<Rigidbody2D>().AddForce(jumpForce, ForceMode2D.Impulse);
            player.GetComponentInChildren<Animator>().SetTrigger("jumping");
        }

        if (stopped){
            apex = true;
            return;
        }

        if (budTimer > budTime){
            bud.SetActive(false);
            budTimer = 0;
        }
        budTimer += Time.deltaTime;

        if (transform.position.y < initY + maxHeigth)
        {
            checkObstacles();
            transform.Translate(Vector2.up * Time.deltaTime * growthSpeed * GameManager.customTimeScale);
            stem.transform.localScale += Vector3.up * Time.deltaTime * growthSpeed * GameManager.customTimeScale;
            if (stem.transform.localScale.y > maxHeigth - this.GetComponent<BoxCollider2D>().bounds.extents.y)
            {
                stem.transform.localScale = new Vector3(stem.transform.localScale.x, maxHeigth - this.GetComponent<SpriteRenderer>().sprite.bounds.extents.y * 2 * this.transform.localScale.y, stem.transform.localScale.z);
            }
            Debug.DrawLine(new Vector2(transform.position.x - 5, transform.position.y + gameObject.GetComponent<Collider2D>().bounds.extents.y), new Vector2(transform.position.x + 5, transform.position.y + gameObject.GetComponent<Collider2D>().bounds.extents.y), Color.red);
        }

        Debug.DrawLine(new Vector2(transform.position.x - 5, transform.position.y + gameObject.GetComponent<Collider2D>().bounds.extents.y), new Vector2(transform.position.x + 5, transform.position.y + gameObject.GetComponent<Collider2D>().bounds.extents.y), Color.red);
        Debug.DrawLine(new Vector2(transform.position.x - 5, initY + maxHeigth), new Vector2(transform.position.x + 5, initY + maxHeigth), Color.green);
        if (transform.position.y > initY + maxHeigth)
        {
            transform.position = new Vector2(transform.position.x, initY + maxHeigth);
            stopped = true;
        }

        if (player && !playerFound)
        {
            player = null;
        }
    }


    public void checkObstacles()
    {
        RaycastHit2D hit = Physics2D.Raycast(transform.position + new Vector3(0f, ray_point, 0f), Vector2.up, small_radius, ~layermask_passables); // fire a raycast directly down the player
        if (hit.collider != null)
        {
            Debug.Log(hit.collider.gameObject.name);
            this.stopped = true;
        }

        RaycastHit2D[] hits = Physics2D.RaycastAll(transform.position + new Vector3(0f, ray_point, 0f), Vector2.up, 20 * small_radius, layermask_oneway);
        foreach (RaycastHit2D h in hits)
        {

            if (h.collider != null)
            {
                float sup_1w = h.collider.bounds.max.y + (0.02f * maxHeigth);

                float dim_p = gameObject.GetComponent<Collider2D>().bounds.extents.y;

                maxHeigth = Mathf.Min(sup_1w - initY - dim_p, maxHeigth);

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

    private void OnCollisionStay2D(Collision2D collision)
    {
        playerFound = false;
        if (Vector2.Angle(collision.GetContact(0).normal, Vector2.down) > upArc) return;
        if (collision.gameObject.tag == "Player")
        {
            player = collision.collider.gameObject;
            playerFound = true;
        }
    }
}
