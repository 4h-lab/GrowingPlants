using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Passable :MonoBehaviour
{
    Collider2D collider;
    Bounds bounds;
    Bounds playerBounds;
    Transform player;
    float shellDistance = 0.1f;

    // Start is called before the first frame update
    void Start()
    {
        collider = this.GetComponent<Collider2D>();
        try
        {
            bounds = this.GetComponent<SpriteRenderer>().sprite.bounds;
        }catch(MissingComponentException e)
        {
            bounds = this.GetComponentInChildren<SpriteRenderer>().sprite.bounds;
        }
        player = GameObject.FindGameObjectWithTag("Player").transform;
        playerBounds = GameObject.FindGameObjectWithTag("Player").GetComponent<Collider2D>().bounds;
    }

    // Update is called once per frame
    void FixedUpdate()
    {
        var playerPos = player.position.y - (playerBounds.extents.y) + shellDistance;
        var thisPos = this.transform.position.y + bounds.extents.y * this.transform.localScale.y;
        if (playerPos >= thisPos)
            {
            /*Debug.Log(playerPos + "<=" + thisPos);
            Debug.DrawLine(player.position, playerPos * Vector3.up);
            Debug.DrawLine(this.transform.position, Vector3.up * thisPos);*/
            //collider.enabled = true;
            collider.isTrigger = false;
            }
            else
            {
            collider.isTrigger = true;
            //collider.enabled = false;
            }
        /*
        else if (playerPos > 0 && thisPos > 0)
        {

            if (playerPos >= thisPos)
            {
                Debug.Log(playerPos + "<=" + thisPos);
                Debug.DrawLine(player.position, playerPos * Vector3.up);
                Debug.DrawLine(this.transform.position, Vector3.up * thisPos);
                collider.enabled = true;
            }
            else
            {
                collider.enabled = false;
            }
        }*/
    }
    }

    


