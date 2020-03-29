using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using System.Linq;

public class CameraMovement : MonoBehaviour{
    public float horizontalRatioBeforeScrolling;
    public float verticalRatioBeforeScrolling;
    public float cameraspeed;
    public bool follow = true;

    private float minX;
    private float maxX;
    private float minY;
    private float maxY;

    private bool canMoveAlongX = true;
    private bool canMoveAlongY = true;

    private Transform playerTransform;


    // Start is called before the first frame update
    void Start(){
        GameObject[] gs = GameObject.FindGameObjectsWithTag("CameraBoundary");
        float[] xs = (from g in gs select g.transform.position.x).ToArray();
        float[] ys = (from g in gs select g.transform.position.y).ToArray();

        minX = Mathf.Min(xs) + Camera.main.orthographicSize * Camera.main.aspect;
        minY = Mathf.Min(ys) + Camera.main.orthographicSize ;
        maxX = Mathf.Max(xs) - Camera.main.orthographicSize * Camera.main.aspect;
        maxY = Mathf.Max(ys) - Camera.main.orthographicSize;

        if (minX >= maxX) {
            canMoveAlongX = false;
            Debug.Log("Can't move along X axis....");
        }
        if (minY >= maxY) {
            canMoveAlongY = false;
            Debug.Log("Can't move along Y axis....");
        }
        playerTransform = GameObject.FindGameObjectWithTag("Player").transform;
    }

    // Update is called once per frame
    void Update(){
        if (!follow) return;

        Vector2 relativePlayerPos = Camera.main.WorldToViewportPoint(playerTransform.position);
        if ((relativePlayerPos.y < verticalRatioBeforeScrolling) || (relativePlayerPos.y > 1 - verticalRatioBeforeScrolling) || (relativePlayerPos.x < horizontalRatioBeforeScrolling) || (relativePlayerPos.x > 1 - horizontalRatioBeforeScrolling)) {

            //Vector3 newpos = playerTransform.position - transform.position;

            Vector3 dir = Vector3.zero;

            if (canMoveAlongX) {
                dir.x = Mathf.Clamp(playerTransform.position.x, minX, maxX) - transform.position.x;
            }
            if (canMoveAlongY)
            {
                dir.y = Mathf.Clamp(playerTransform.position.y, minY, maxY) - transform.position.y;
            }

            transform.Translate(dir * Time.deltaTime * cameraspeed);

           


            /*
            newpos.z = transform.position.z;
            newpos.x = canMoveAlongX ? (Mathf.Clamp(newpos.x, minX, maxX)) : transform.position.x;
            newpos.y = canMoveAlongY ? (Mathf.Clamp(newpos.y, minY, maxY)) : transform.position.y;

            Debug.Log(transform.position + " <> " + newpos + " <> " + playerTransform.position);

            transform.position = newpos;

            
            /*
            transform.Translate(newpos * Time.deltaTime );
            transform.position = new Vector3(Mathf.Clamp(transform.position.x, minX, maxX), Mathf.Clamp(transform.position.y, minY, maxY), transform.position.z);
            */
        }
        Vector3 deb = transform.position;
        deb.z = 0;

        Debug.DrawLine(new Vector3(minX, minY, 0), new Vector3(maxX, minY, 0), Color.red);
        Debug.DrawLine(new Vector3(minX, maxY, 0), new Vector3(maxX, maxY, 0), Color.red);
        Debug.DrawLine(new Vector3(minX, minY, 0), new Vector3(minX, maxY, 0), Color.red);
        Debug.DrawLine(new Vector3(maxX, minY, 0), new Vector3(maxX, maxY, 0), Color.red);



        Debug.DrawLine(deb + new Vector3(Camera.main.orthographicSize * Camera.main.aspect, 0, 0) + Vector3.left, deb + new Vector3(Camera.main.orthographicSize * Camera.main.aspect, 0, 0) + Vector3.right, Color.blue);
        Debug.DrawLine(deb + new Vector3(Camera.main.orthographicSize * Camera.main.aspect, 0, 0) + Vector3.up, deb + new Vector3(Camera.main.orthographicSize * Camera.main.aspect, 0, 0) + Vector3.down, Color.blue);

        Debug.DrawLine(deb - new Vector3(Camera.main.orthographicSize, 0, 0) + Vector3.left, deb - new Vector3(Camera.main.orthographicSize, 0, 0) + Vector3.right, Color.blue);
        Debug.DrawLine(deb - new Vector3(Camera.main.orthographicSize, 0, 0) + Vector3.up, deb - new Vector3(Camera.main.orthographicSize, 0, 0) + Vector3.down, Color.blue);

        Debug.DrawLine(deb + new Vector3(0, Camera.main.orthographicSize, 0) + Vector3.left, deb + new Vector3(0, Camera.main.orthographicSize, 0) + Vector3.right, Color.blue);
        Debug.DrawLine(deb + new Vector3(0, Camera.main.orthographicSize, 0) + Vector3.up, deb + new Vector3(0, Camera.main.orthographicSize, 0) + Vector3.down, Color.blue);

        Debug.DrawLine(deb - new Vector3(0, Camera.main.orthographicSize, 0) + Vector3.left, deb - new Vector3(0, Camera.main.orthographicSize, 0) + Vector3.right, Color.blue);
        Debug.DrawLine(deb - new Vector3(0, Camera.main.orthographicSize, 0) + Vector3.up, deb - new Vector3(0, Camera.main.orthographicSize, 0) + Vector3.down, Color.blue);

    }
}
