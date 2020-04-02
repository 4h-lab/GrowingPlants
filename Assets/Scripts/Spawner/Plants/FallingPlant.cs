﻿using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class FallingPlant : BasePlant
{
    public float fallDelay = 2f;
    private float timer = 0f;
    // Start is called before the first frame update
    void Start()
    {
        if (this.GetSpawner().GetComponent<FallingPlatformContactInteractable>() != null)
        {

            var tile = this.GetSpawner().GetComponent<FallingPlatformContactInteractable>();
            tile.spawnedPlant(); // make the soil start to shake, so if the character created a plant before actually touching it shakes anyway
            timer = tile.getRemainingTime();
            fallDelay = tile.getDelay();
        }
       // initY = this.transform.position.y;
        ray_point = (this.gameObject.GetComponent<SpriteRenderer>().sprite.bounds.extents.y * this.transform.localScale.y) + small_radius;
        ee = GameObject.FindGameObjectWithTag("EventEmitter").GetComponent<EventEmitter>();
        ee.invoke("plant_created", (new[] { this.gameObject }));

    }

    private void FixedUpdate(){
        timer += Time.deltaTime * GameManager.customTimeScale;
        if (timer >= fallDelay) { stopped = true; FallAfterDelay(); }
    }
    void FallAfterDelay(){
        this.gameObject.AddComponent<Rigidbody2D>();
        Destroy(this.gameObject.GetComponent<BoxCollider2D>());
    }


}
