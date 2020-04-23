using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[RequireComponent(typeof(PlatformEffector2D))]
public class AngleUpdate : MonoBehaviour
{
    // Start is called before the first frame update
    Collider2D collider;
    Bounds bounds;
    Bounds playerBounds;
    Transform player;
    float shellDistance = 0.1f;
    PlatformEffector2D platform;

    // Start is called before the first frame update
    void Start()
    {
        collider = this.GetComponent<Collider2D>();
        
       bounds = this.GetComponent<Collider2D>().bounds;
        
        player = GameObject.FindGameObjectWithTag("Player").transform;
        playerBounds = GameObject.FindGameObjectWithTag("Player").GetComponent<Collider2D>().bounds;
        platform = this.GetComponent<PlatformEffector2D>();
    }

    private void Update()
    {
        var playerPos = player.position.y - (playerBounds.extents.y);
        var thisPos = this.transform.position.y + bounds.extents.y ;
        if (playerPos >= thisPos)
        {
            /*Debug.Log(playerPos + "<=" + thisPos);
            Debug.DrawLine(player.position, playerPos * Vector3.up);
            Debug.DrawLine(this.transform.position, Vector3.up * thisPos);*/
            //collider.enabled = true;
            platform.surfaceArc = 180f;
        }
        else
        {
            platform.surfaceArc = 150f;
            //collider.enabled = false;
        }
    }
}
