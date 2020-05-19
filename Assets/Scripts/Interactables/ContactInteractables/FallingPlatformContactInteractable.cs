using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class FallingPlatformContactInteractable : BaseCollisionInteractable, IInteractableWithEvents
{
    [SerializeField]
    private float fallDelay = 2.0f;
    [SerializeField]
    private float shakeIntensity = 1.0f;
    private bool shaking = false;
    private Vector3[] originalPosition;
    private float timer = 0f;

    private EventEmitter ee;

    private void Start() {
        ee = gameObject.AddComponent(typeof(EventEmitter)) as EventEmitter;

        originalPosition = new Vector3[this.transform.childCount];
        for (int j = 0; j < this.transform.childCount; j++) {
            originalPosition[j] = this.gameObject.transform.GetChild(j).transform.position;
        }

    }
    private void FixedUpdate() {
        if (shaking) {
            var r = (Random.insideUnitSphere * Time.deltaTime * GameManager.customTimeScale * shakeIntensity);
            for (int i = 0; i < this.transform.childCount; i++) {
                this.gameObject.transform.GetChild(i).transform.position = originalPosition[i] + r; //Added a * 3 to increase the trembling effect (Lorenzo)
            }
            timer += Time.deltaTime * GameManager.customTimeScale;
            if (timer >= fallDelay) FallAfterDelay();
        }

    }

    public override void interact(GameObject initiator) {
        if (initiator.tag == "Player") {
            shaking = true;
            ParticleSystem ps = GetComponentInChildren<ParticleSystem>(true);
            if (ps != null) ps.Play();
        }
    }
    void FallAfterDelay() {
        ee.invoke("plant.falling", new Object[]{ this} );
        shaking = false;
        this.gameObject.AddComponent<Rigidbody2D>();

        Destroy(this.gameObject.GetComponent<BoxCollider2D>());
    }

    public float getRemainingTime() {
        return timer;
    }
    public float getDelay() {
        return fallDelay;
    }

    public void spawnedPlant() {
        this.shaking = true;
    }

    public void subsribeEvent(string eventname, EventEmitter.EventCallback ev) {
        ee.on(eventname, ev);
    }
}
