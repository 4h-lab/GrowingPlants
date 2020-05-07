using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class WaypointsMovement : MonoBehaviour{
    public List<GameObject> wayPoints;
    public float speed;
    public float minDesiredDistanceToWaypoint = .01f;
    [SerializeField] ContactFilter2D collisionFilter;

    private int wayPointIndex = 0;
    private Vector2 nextWayPoint;
    private int direction = 1;

    private Rigidbody2D rb;
    private List<Vector2> realWayPoints;
    bool stop = false;


    // Start is called before the first frame update
    void Start(){
        rb = GetComponent<Rigidbody2D>();
        wayPoints.Add(gameObject);
        realWayPoints = new List<Vector2>(wayPoints.Count);

        for (int i = 0; i < wayPoints.Count; i++) {
            realWayPoints.Add((Vector2)(wayPoints[i].transform.position));
        }
        nextWayPoint = realWayPoints[wayPointIndex];
    }

    // Update is called once per frame
    void Update(){
        float desiredDistance = Mathf.Min((nextWayPoint - (Vector2)transform.position).magnitude, speed * Time.deltaTime * GameManager.customTimeScale);
        Vector2 nextPosition = (nextWayPoint - (Vector2)transform.position).normalized;
        RaycastHit2D[] castResults = new RaycastHit2D[16];

        int collisionCount = rb.Cast(nextPosition, collisionFilter, castResults, desiredDistance);
        Debug.Log("count " + collisionCount);
        if (collisionCount > 0)
        {
            Debug.Log("COLLISION");
            stop = true;
            ChangeDirection();
            
            nextPosition = (nextWayPoint - (Vector2)transform.position).normalized;
        }

        transform.Translate(nextPosition * desiredDistance);
        if (Vector2.Distance(transform.position, nextWayPoint) < minDesiredDistanceToWaypoint) {
            ChangeWaypoint();
        } 
    }

    int ChangeDirection()
    {
        direction = -direction;
        ChangeWaypoint();
        return direction;
    }

    int ChangeWaypoint()
    {
        wayPointIndex = (wayPoints.Count + wayPointIndex + direction) % wayPoints.Count;
        nextWayPoint = realWayPoints[wayPointIndex];
        return wayPointIndex;
    }
}
