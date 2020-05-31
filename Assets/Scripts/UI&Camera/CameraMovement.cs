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


    private GameManager gm;

    float touchtime=0;


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

        Debug.Log(Mathf.Min(xs) +"  " + Mathf.Max(xs) + " " + Camera.main.orthographicSize);

        if (minX >= maxX) {
            canMoveAlongX = false;
            
        }
        if (minY >= maxY) {
            canMoveAlongY = false;
            
        }
        playerTransform = GameObject.FindGameObjectWithTag("Player").transform;
        
        gm = GameObject.FindObjectOfType<GameManager>();
    }

    // Update is called once per frame
    void Update(){
        if (!follow) return;

        Vector2 relativePlayerPos = Camera.main.WorldToViewportPoint(playerTransform.position);
        Vector3 dir = Vector3.zero;

        if (canMoveAlongX && ((relativePlayerPos.x < horizontalRatioBeforeScrolling) || (relativePlayerPos.x > 1 - horizontalRatioBeforeScrolling))) {
            dir.x = Mathf.Clamp(playerTransform.position.x, minX, maxX) - transform.position.x;
        }
        if (canMoveAlongY && ((relativePlayerPos.y < verticalRatioBeforeScrolling) || (relativePlayerPos.y > 1 - verticalRatioBeforeScrolling))) { 
            dir.y = Mathf.Clamp(playerTransform.position.y, minY, maxY) - transform.position.y;
        }
        
        //Debug.Log(Camera.main.velocity.ToString());
        transform.Translate(dir * Time.fixedDeltaTime* cameraspeed);

           


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
        



        Vector3 deb = transform.position;
        deb.z = 0;

        Debug.DrawLine(new Vector3(minX, minY, 0), new Vector3(maxX, minY, 0), Color.red);
        Debug.DrawLine(new Vector3(minX, maxY, 0), new Vector3(maxX, maxY, 0), Color.red);
        Debug.DrawLine(new Vector3(minX, minY, 0), new Vector3(minX, maxY, 0), Color.red);
        Debug.DrawLine(new Vector3(maxX, minY, 0), new Vector3(maxX, maxY, 0), Color.red);

        Debug.DrawLine(Camera.main.ViewportToWorldPoint(new Vector2(verticalRatioBeforeScrolling, 0 )), Camera.main.ViewportToWorldPoint(new Vector2(verticalRatioBeforeScrolling, 1)), Color.green);
        Debug.DrawLine(Camera.main.ViewportToWorldPoint(new Vector2(1-verticalRatioBeforeScrolling, 0)), Camera.main.ViewportToWorldPoint(new Vector2(1-verticalRatioBeforeScrolling, 1)), Color.green);

        Debug.DrawLine(Camera.main.ViewportToWorldPoint(new Vector2(0, horizontalRatioBeforeScrolling)), Camera.main.ViewportToWorldPoint(new Vector2(1, horizontalRatioBeforeScrolling)), Color.green);
        Debug.DrawLine(Camera.main.ViewportToWorldPoint(new Vector2(0, 1- verticalRatioBeforeScrolling)), Camera.main.ViewportToWorldPoint(new Vector2(1, 1-horizontalRatioBeforeScrolling)), Color.green);




        Debug.DrawLine(deb + new Vector3(Camera.main.orthographicSize * Camera.main.aspect, 0, 0) + Vector3.left, deb + new Vector3(Camera.main.orthographicSize * Camera.main.aspect, 0, 0) + Vector3.right, Color.blue);
        Debug.DrawLine(deb + new Vector3(Camera.main.orthographicSize * Camera.main.aspect, 0, 0) + Vector3.up, deb + new Vector3(Camera.main.orthographicSize * Camera.main.aspect, 0, 0) + Vector3.down, Color.blue);



    }

    private void LateUpdate()
    {

        if (Input.touchCount >0 && gm.isPaused)
        {
            if (Input.GetTouch(0).phase == TouchPhase.Began)
            {
                touchtime = Time.realtimeSinceStartup;
            }
            else if ((Input.GetTouch(0).phase == TouchPhase.Moved || Input.GetTouch(0).phase == TouchPhase.Stationary)&& Time.realtimeSinceStartup-touchtime>0.2f)
            { 
                var finger = Input.GetTouch(0).position;
                Vector2 relativePlayerPos = finger;
                Vector3 dir = Vector3.zero;

                if (canMoveAlongX && ((relativePlayerPos.x < horizontalRatioBeforeScrolling) || (relativePlayerPos.x > 1 - horizontalRatioBeforeScrolling)))
                {
                    dir.x = Mathf.Clamp(Camera.main.ScreenToWorldPoint(finger).x, minX, maxX) - transform.position.x;
                }
                if (canMoveAlongY && ((relativePlayerPos.y < verticalRatioBeforeScrolling) || (relativePlayerPos.y > 1 - verticalRatioBeforeScrolling)))
                {
                    dir.y = Mathf.Clamp(Camera.main.ScreenToWorldPoint(finger).y, minY, maxY) - transform.position.y;
                }

                transform.Translate(dir * Time.fixedDeltaTime * cameraspeed);
            }
            else if(Input.GetTouch(0).phase == TouchPhase.Ended)
            {
                touchtime = 0f;
            }

        }
    }

    public Vector4 levelBounds()
    {
        return new Vector4(minX, maxX, minY, maxY);
    }

    

}
