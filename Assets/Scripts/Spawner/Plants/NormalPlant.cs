using System.Collections;
using System.Collections.Generic;
using System.Linq;
using UnityEngine;

public class NormalPlant : BasePlant{
    private int layermask_passables;
    private int layermask_oneway;
    private GameObject stem;
    private GameObject bud;
    public float budTime = 2f;
    private float budTimer=0f;

    private Vector3 point;

    void Start(){
        layermask_passables = (1 << LayerMask.NameToLayer("plant")) | (1 << LayerMask.NameToLayer("passable")) | (1 << 2) | 1 << LayerMask.NameToLayer("onewayplatform");
        layermask_oneway = 1 << LayerMask.NameToLayer("onewayplatform");

        ray_point = (this.gameObject.GetComponent<BoxCollider2D>().bounds.extents.y + small_radius);

        stem = this.transform.GetChild(0).gameObject;
        bud = this.transform.GetChild(1).gameObject;
        stem.transform.localScale -= new Vector3(0f, stem.transform.localScale.y-ray_point, 0f);
        checkEdges();
        initY = this.transform.position.y;
        
        ee = GameObject.FindGameObjectWithTag("EventEmitter").GetComponent<EventEmitter>();
        ee.invoke("plant_created", (new[] { this.gameObject }));

        GameObject.FindGameObjectWithTag("GameManager").GetComponent<GameManager>().notifyOfNewSomething("plant.planted");
    }

    void Update(){
        if(budTimer> budTime)
        {
            bud.SetActive(false);
            budTimer = 0;
        }
        budTimer += Time.deltaTime;
        var step = Vector2.up * Time.deltaTime * growthSpeed * GameManager.customTimeScale;
        if (transform.position.y < initY + maxHeigth && !stopped){
            checkObstacles();
            
            transform.Translate(step);
           
            Debug.DrawLine(new Vector2(transform.position.x - 5, transform.position.y + gameObject.GetComponent<Collider2D>().bounds.extents.y), new Vector2(transform.position.x + 5, transform.position.y + ray_point), Color.red);
        }
        growStem(step);
        //Debug.DrawLine(new Vector2(transform.position.x - 5, transform.position.y + gameObject.GetComponent<Collider2D>().bounds.extents.y), new Vector2(transform.position.x + 5, transform.position.y + gameObject.GetComponent<Collider2D>().bounds.extents.y), Color.red);
        //Debug.DrawLine(new Vector2(transform.position.x - 5, initY+maxHeigth), new Vector2(transform.position.x + 5, initY + maxHeigth), Color.green);
        if (transform.position.y > initY + maxHeigth)
        {
            stopped = true;
            transform.position = new Vector2(transform.position.x, initY + maxHeigth);
            
        }

        
    }
    /// <summary>
    /// soluzione temporanea per le piante che nascono nel nulla. controlla se a destra ci sono oggetti, se ci sono flippa sulle x la sprite
    /// </summary>
    private void checkEdges()
    {
        /*RaycastHit2D hit = Physics2D.Raycast(transform.position + new Vector3(this.gameObject.GetComponent<BoxCollider2D>().bounds.extents.x/2, ray_point, 0f), -Vector2.up, small_radius, ~layermask_passables);
         if (hit.collider==null)
            {
            Debug.Log("wollo");
            stem.GetComponent<SpriteRenderer>().flipX =true;
            }  */
        RaycastHit2D[] hits = Physics2D.BoxCastAll(transform.position, GetComponent<BoxCollider2D>().bounds.extents * 2, 0, -Vector2.up, ~layermask_passables);
        var d = new Dictionary<RaycastHit2D, float>();
        foreach (RaycastHit2D hit in hits)
        {
            d.Add(hit, hit.distance);
        }
        d.OrderBy(item => item.Value);
        
        
        var p1= GameObject.FindGameObjectWithTag("Player").transform.position;
        var p2 = (Vector3)d.First().Key.point;
        float angle = 0;
        if (p1.x > p2.x+0.1f)
        {
            angle = -2;
            stem.GetComponent<SpriteRenderer>().flipX = true;
        }
        else if(p1.x < p2.x -0.1f)
        {
            angle = 2;
        }
        
        stem.transform.eulerAngles = new Vector3(0f,0f,angle);

        point = (Vector3)nearest.point;



    }

    /// <summary>
    /// modifica la scala dell'oggetto stem in modo da simulare la crescita della pianta. La scala viene settata alla posizione della del bc2D.
    /// L'oggetto stem ha settato il pivot a top, quindi l'aumento di scala parte dall'alto e non dal centro. Molplica lo step per una costante in 
    /// modo da rellentarlo per non farlo fuoriuscire.
    /// </summary>
    private void growStem(Vector3 step)
    {
           
            if (stem.transform.localScale.y <=maxHeigth -ray_point*2) 
        {
                stem.transform.localScale += step*0.9f;
                //Debug.Log(stem.transform.localScale.y);
                //stem.transform.localScale = new Vector3(stem.transform.localScale.x, maxHeigth - this.transform.Find("Flower").GetComponent<SpriteRenderer>().sprite.bounds.extents.y  /** this.transform.Find("Flower").localScale.y*/, stem.transform.localScale.z);
                //stem.transform.localScale = new Vector3(stem.transform.localScale.x, maxHeigth - ray_point * 2, stem.transform.localScale.z);
        }
        
    }
    /// <summary>
    /// controllo sugli ostacoli: prima cerca oggetti non passable in modo da fermarsi quando li tocca, poi cerca oggetti oneway
    /// per aggiornare la sua maxheigth
    /// </summary>
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
        Gizmos.color = Color.blue;
        Gizmos.DrawLine(transform.position , point);
    }
}
