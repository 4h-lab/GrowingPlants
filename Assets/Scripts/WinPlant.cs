using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class WinPlant : MonoBehaviour
{
    public float maxHeigth;
    public float growthSpeed;

    private float initY;

    private EventEmitter ee;
    private GameObject player;
    private bool stopped = false;
    private float ray_point;
    private float small_radius = 0.01f;

    void Start()
    {
        ee = GameObject.FindGameObjectWithTag("EventEmitter").GetComponent<EventEmitter>();
        ee.invoke("plant_created", (new[] { this.gameObject }));
        player = GameObject.Find("Player");

        //TODO: block camera
        Destroy(player.GetComponent<Rigidbody2D>());
        Destroy(player.GetComponent<BoxCollider2D>());
        Destroy(GameObject.Find("SquishCollider"));
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
        player.transform.Translate(Vector2.up * Time.deltaTime * growthSpeed * GameManager.customTimeScale);
        gameObject.transform.Translate(Vector2.up * Time.deltaTime * growthSpeed * GameManager.customTimeScale);
    }
}
