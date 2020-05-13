using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class FallingPlant : BasePlant
{
    public float fallDelay = 2f;
    //private float timer = 0f;
    [SerializeField]
    private float shakeIntensityStem = 1f;
    [SerializeField]
    private float shakeIntensityFlower = 0.5f;
    [SerializeField]
    private bool moveFlower = true;
    [SerializeField]
    private bool allDirectionForFlower = true;
    private Vector3[] originalPosition = new Vector3[2];
    private GameObject[] stemflower = new GameObject[2];

    // Start is called before the first frame update
    void Start() {
        stemflower[0] = this.transform.Find("Stem").gameObject;
        stemflower[1] = this.transform.Find("Flower").gameObject;
        originalPosition = new Vector3[this.transform.childCount];
        for (int j = 0; j < stemflower.Length; j++) {
            originalPosition[j] = stemflower[j].transform.position;
        }
        if (this.GetSpawner().GetComponent<FallingPlatformContactInteractable>() != null) {

            var tile = this.GetSpawner().GetComponent<FallingPlatformContactInteractable>();
            tile.spawnedPlant(); // make the soil start to shake, so if the character created a plant before actually touching it shakes anyway
            //timer = tile.getRemainingTime();
            fallDelay = tile.getDelay();
        }
        // initY = this.transform.position.y;
        ray_point = (this.gameObject.GetComponent<SpriteRenderer>().sprite.bounds.extents.y * this.transform.localScale.y) + small_radius;
        ee = GameObject.FindGameObjectWithTag("EventEmitter").GetComponent<EventEmitter>();
        ee.invoke("plant_created", (new[] { this.gameObject }));


        this.GetSpawner().GetComponent<IInteractableWithEvents>()?.subsribeEvent("plant.falling", FallAfterDelay);

    }

    private void FixedUpdate() {
        /*
        timer += Time.deltaTime * GameManager.customTimeScale;
        if (timer >= fallDelay && !stopped) { stopped = true; FallAfterDelay(); }
        */
    }

    private void LateUpdate() {
        for (int j = 0; j < stemflower.Length; j++) {
            originalPosition[j] = stemflower[j].transform.position;
        }
        if (allDirectionForFlower && moveFlower) {
            float step = shakeIntensityFlower * Time.deltaTime;
            stemflower[1].transform.position = Vector3.MoveTowards(originalPosition[1], originalPosition[1] + Random.insideUnitSphere, step);
        } else if (moveFlower) {
            var steppo = shakeIntensityFlower * Time.deltaTime;
            var l = new Vector3(Random.value - .5f, 0f, 0f);
            stemflower[1].transform.position = Vector3.MoveTowards(originalPosition[1], originalPosition[1] + l, steppo);
        }

        var steps = shakeIntensityStem * Time.deltaTime;
        var r = new Vector3(Random.value - .5f, 0f, 0f);
        stemflower[0].transform.position = Vector3.MoveTowards(originalPosition[0], originalPosition[0] + r, steps);





    }

    void FallAfterDelay(Object[] par) {
        this.FallAfterDelay();
        Debug.Log("DIO EMRDA");
    }
    void FallAfterDelay() {

        for (int j = 0; j < this.transform.childCount; j++) {
            var r = this.gameObject.transform.GetChild(j).gameObject.AddComponent<Rigidbody2D>();
            r.AddForce(Random.insideUnitSphere * 2, ForceMode2D.Impulse);
        }
        Destroy(this.gameObject.GetComponent<BoxCollider2D>());
        Destroy(this.gameObject.GetComponentInChildren<BoxCollider2D>());
        Destroy(this.transform.Find("SecurityBox").gameObject);

    }


}
