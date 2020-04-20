using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class FallingPlant : BasePlant
{
    public float fallDelay = 2f;
    private float timer = 0f;
    [SerializeField]
    private float shakeIntensity = 1f;
    [SerializeField]
    private int toMove = 1;
    private Vector3[] originalPosition;

    // Start is called before the first frame update
    void Start()
    {
        originalPosition = new Vector3[this.transform.childCount];
        for (int j = 0; j < this.transform.childCount; j++)
        {
            originalPosition[j] = this.gameObject.transform.GetChild(j).transform.position;
        }
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
        if (timer >= fallDelay && !stopped) { stopped = true; FallAfterDelay(); }
    }

    private void LateUpdate()
    {
        for (int j = 0; j < this.transform.childCount; j++)
        {
            originalPosition[j] = this.gameObject.transform.GetChild(j).transform.position;
        }

        for (int i = 0; i < toMove; i++)
        {

            float step = shakeIntensity * Time.deltaTime;
            Vector3 r = new Vector3(Random.value - .5f, 0f, 0f);
            this.transform.GetChild(i).position = Vector3.MoveTowards(originalPosition[i], originalPosition[i] + r, step);
        }


    }
    void FallAfterDelay(){
        for (int j = 0; j < this.transform.childCount; j++)
        {
            var r = this.gameObject.transform.GetChild(j).gameObject.AddComponent<Rigidbody2D>();
            r.AddForce(Random.insideUnitSphere*2, ForceMode2D.Impulse);
        }
        
        Destroy(this.gameObject.GetComponent<BoxCollider2D>());
    }


}
