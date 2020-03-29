using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using System.Linq;

public class CameraMovement : MonoBehaviour{
    public float horizontalRatioBeforeScrolling;
    public float verticalRatioBeforeScrolling;

    private float minX;
    private float maxX;
    private float minY;
    private float maxY;

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

        playerTransform = GameObject.FindGameObjectWithTag("Player").transform;

        
    }

    // Update is called once per frame
    void Update(){
        Vector2 relativePlayerPos = Camera.main.WorldToViewportPoint(playerTransform.position);
        if ((relativePlayerPos.y < verticalRatioBeforeScrolling) || (relativePlayerPos.y > 1 - verticalRatioBeforeScrolling) || (relativePlayerPos.x < horizontalRatioBeforeScrolling) || (relativePlayerPos.x > 1 - horizontalRatioBeforeScrolling)) {

            Vector3 newpos = playerTransform.position - transform.position;
            newpos.z = 0;

            transform.Translate(newpos * Time.deltaTime );
            transform.position = new Vector3(Mathf.Clamp(transform.position.x, minX, maxX), Mathf.Clamp(transform.position.y, minY, maxY), transform.position.z);

        }
        /*
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
        */

    }
}
