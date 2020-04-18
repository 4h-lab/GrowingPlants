using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class WaypointsMovement : MonoBehaviour{
    public List<GameObject> wayPoints;
    public float speed;
    public float minDesiredDistanceToWaypoint = .01f;

    private int wayPointIndex = 0;
    private Vector2 nextWayPoint;

    private List<Vector2> realWayPoints;


    // Start is called before the first frame update
    void Start(){
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
        transform.Translate((nextWayPoint - (Vector2)transform.position).normalized * desiredDistance) ;
        if (Vector2.Distance(transform.position, nextWayPoint) < minDesiredDistanceToWaypoint) {
            wayPointIndex = (wayPointIndex + 1) % wayPoints.Count;
            nextWayPoint = realWayPoints[wayPointIndex]; 
        } 
    }
}
