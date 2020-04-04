using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class WaypointsMovement : MonoBehaviour{
    public List<GameObject> wayPoints;
    public float speed;


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
        transform.Translate((nextWayPoint - (Vector2)transform.position).normalized * speed * Time.deltaTime * GameManager.customTimeScale ) ;
        if (Vector2.Distance(transform.position, nextWayPoint) < 0.01f) {
            wayPointIndex = (wayPointIndex + 1) % wayPoints.Count;
            nextWayPoint = realWayPoints[wayPointIndex]; 
        } 
    }
}
