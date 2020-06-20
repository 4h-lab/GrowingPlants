using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class WinPlant : BasePlant
{
    void Start()
    {
        FindObjectOfType<GameManager>().setWinPause(true);
        ee = GameObject.FindGameObjectWithTag("EventEmitter").GetComponent<EventEmitter>();
        ee.invoke("plant_created", (new[] { this.gameObject }));
        player = GameObject.FindGameObjectWithTag("Player");

        //FindObjectOfType<SmoothCamera2D>().enabled = false;
        player.GetComponent<Rigidbody2D>().isKinematic = true;
        player.GetComponent<BoxCollider2D>().isTrigger = true;
    }

    void Update()
    {
        if (GetComponent<SpriteRenderer>().isVisible) FlyAway();
    }

    private void OnBecameInvisible()
    {
        ee.invoke("win", (new[] { this.gameObject }));
    }

    private void FlyAway()
    {
        
        player.transform.Translate(Vector2.up * Time.deltaTime * growthSpeed);
        gameObject.transform.Translate(Vector2.up * Time.deltaTime * growthSpeed);
    }
}
