using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using System.Linq;

public class NormalPlant : BasePlant, IFallInteractable{
    private int layermask_passables;
    private int layermask_oneway;
    private int layermask_stem;
    private GameObject stem;
    private GameObject bud;
    public float budTime = 2f;
    private float budTimer=0f;

    void Start(){
        layermask_passables = (1 << LayerMask.NameToLayer("plant")) | (1 << LayerMask.NameToLayer("passable")) | (1 << 2) | 1 << LayerMask.NameToLayer("onewayplatform") | (1 << LayerMask.NameToLayer("collectible") | (1 << LayerMask.NameToLayer("water")));
        layermask_oneway = 1 << LayerMask.NameToLayer("onewayplatform");
        layermask_stem = (1 << LayerMask.NameToLayer("plant")) | (1 << 2);

        stem = this.transform.GetChild(0).gameObject;
        bud = this.transform.GetChild(1).gameObject;
        stem.transform.localScale -= new Vector3(0f,stem.transform.localScale.y,0f);
        initY = this.transform.position.y;
        ray_point= (this.gameObject.GetComponent<BoxCollider2D>().bounds.extents.y +small_radius);

        checkEdges();
        ee = GameObject.FindGameObjectWithTag("EventEmitter").GetComponent<EventEmitter>();
        ee.invoke("plant_created", (new[] { this.gameObject }));

        GameObject.FindGameObjectWithTag("GameManager").GetComponent<GameManager>().notifyOfNewSomething("plant.planted");

        ParticleSystem ps = transform.Find("StartPS").GetComponent<ParticleSystem>();//GetComponentInChildren<ParticleSystem>();
        if (ps != null) {
            ps.transform.parent = null;
            ps?.Play();
            StartCoroutine(ensureParticleSystemDestroyed(ps));
        }
    }

    void Update()
    {
        if (stopped) return;

        if(budTimer> budTime)
        {
            bud.SetActive(false);
            // budTimer = 0;
        }
        budTimer += Time.deltaTime;

        if (transform.position.y < initY + maxHeigth){
            checkObstacles();
            var step = Vector2.up * Time.deltaTime * growthSpeed * GameManager.customTimeScale;
            transform.Translate(step);
            growStem(step);
        }

        
        if (transform.position.y > initY + maxHeigth)
        {
            transform.position = new Vector2(transform.position.x, initY + maxHeigth);
            
        }
        

    }

    private void checkEdges()
    {
        
        RaycastHit2D[] hits = Physics2D.BoxCastAll(transform.position, GetComponent<BoxCollider2D>().bounds.extents * 2, 0, -Vector2.up, ~layermask_passables);
        var d = new Dictionary<RaycastHit2D, float>();
        foreach (RaycastHit2D hit in hits)
        {
             if (hit.normal.x == 0f)
            {
                d.Add(hit, hit.distance);
            }
          
        }
        d.OrderBy(item => item.Value);


        var p1 = GameObject.FindGameObjectWithTag("Player").transform.position;
        var p2 = (Vector3)d.First().Key.point;
        var diff = p2.x - p1.x;
        if (p1.x > p2.x)
        {
            
            stem.GetComponent<SpriteRenderer>().flipX = true;
        }


        stem.transform.position = new Vector2(this.transform.position.x + diff/8, this.transform.position.y);



    }

    private void growStem(Vector3 step)
    {
        if (!stopped)
        {
            if (stem.transform.localScale.y <= maxHeigth *2)//- ray_point * 2)
            {
                stem.transform.localScale += step * 2f;
                
            }
        }
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

            if (h.collider != null) {
                float sup_1w = h.collider.bounds.max.y + (0.02f * maxHeigth);

                float dim_p = gameObject.GetComponent<Collider2D>().bounds.extents.y;

                maxHeigth = Mathf.Min(sup_1w - initY -dim_p, maxHeigth); 

                Debug.Log(name + " collided with: " + h.collider.gameObject.name + " new: " + maxHeigth) ;
            }
        }
        
    }
    private void OnDrawGizmos(){
        Gizmos.color = Color.red;
        //Gizmos.DrawLine(this.transform.position, this.transform.position + new Vector3(0f, radius, 0f));
        Gizmos.DrawLine(transform.position + new Vector3(0f, ray_point, 0f), transform.position + new Vector3(0f, ray_point+0.01f, 0f));
    }

    private void OnCollisionEnter2D(Collision2D collision)
    {
        if (collision.collider.gameObject.tag == "Player")
        {
            collision.collider.transform.SetParent(this.transform);
            collision.collider.transform.SetAsLastSibling();
        }
    }

    private void OnCollisionExit2D(Collision2D collision)
    {
        if (collision.collider.gameObject.tag == "Player")
        {
            collision.collider.transform.SetParent(null);
        }
    }

    private IEnumerator ensureParticleSystemDestroyed(ParticleSystem ps, float time = 1) {
        yield return new WaitForSeconds(time);
        Destroy(ps.gameObject);
    }

    public void fallInteract(GameObject initiator) {
        StartCoroutine(boing(1f, transform.Find("Flower").transform));
    }
    private IEnumerator boing(float t, Transform trans ) {
        if(budTime > budTimer) yield break;
        transform.Find("FallPS")?.GetComponent<ParticleSystem>()?.Play();

        float actual_time = 0;
        Vector3 startpos = trans.position;
        while (actual_time < t) {
            actual_time += Time.deltaTime;
            trans.position = new Vector3(startpos.x, startpos.y - (Mathf.Sin(actual_time * 10) / (30* actual_time)), startpos.z);
            yield return new WaitForEndOfFrame();
        }
        trans.position = startpos;
    }
}
