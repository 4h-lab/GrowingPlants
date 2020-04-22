using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class FallingPlant : BasePlant
{
    public float fallDelay = 2f;
    private float timer = 0f;
    [SerializeField]
    private float shakeIntensityStem = 1f;
    [SerializeField]
    private float shakeIntensityFlower = 0.5f;
    [SerializeField]
    private bool moveFlower = true;
    [SerializeField]
    private bool allDirectionForFlower = true;
    private GameObject[] stemflower = new GameObject[2];

    private Vector3 offset;
    // Start is called before the first frame update
    void Start()
    {
        stemflower[0] = this.transform.Find("Stem").gameObject;
        stemflower[1] = this.transform.Find("Flower").gameObject;

        offset = this.transform.position- stemflower[1].transform.position;

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
        // shaking della pianta e dello stelo. Fatto in late update almeno viene effettuato dopo che la pianta è cresciuta.
        //Si basa sull posizione del box collider dell'oggetto padre.
    {
        for (int j = 0; j < stemflower.Length; j++)
        {

            
            if (allDirectionForFlower && moveFlower)
            {
                float step = shakeIntensityFlower * Time.deltaTime;
                //stemflower[1].transform.position = Vector3.MoveTowards(this.transform.position- offset, this.transform.position -offset + Random.insideUnitSphere, step);
                stemflower[1].transform.position = this.transform.position - offset + (Random.insideUnitSphere * shakeIntensityFlower);
            }
            else if (!allDirectionForFlower && moveFlower)
            {
                var steppo = shakeIntensityFlower * Time.deltaTime;
                var l = new Vector3(Random.value - .5f, 0f, 0f);
                stemflower[1].transform.position = this.transform.position - offset + (l * shakeIntensityFlower);
            }

            var steps = shakeIntensityStem * Time.deltaTime;
            var r = new Vector3(Random.value - .5f, 0f, 0f);
            //stemflower[0].transform.position = Vector3.MoveTowards(originalPosition[0], originalPosition[0] + r, steps);
            stemflower[0].transform.position = this.transform.position - offset + (r * shakeIntensityStem);

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
