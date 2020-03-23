using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class FallingPlatformContactInteractable : BaseCollisionInteractable
{
    // Start is called before the first frame update
    public float fallDelay = 2.0f;

    private bool shaking=false;
    private Vector3 originalPosition;
    private float timer=0f;

    private void Start()
    {
        originalPosition = this.transform.position;
    }
    private void FixedUpdate()
    {
        if (shaking)
        {
            this.transform.position = originalPosition + (Random.insideUnitSphere * Time.deltaTime);
            timer += Time.deltaTime;
            if (timer >= fallDelay) FallAfterDelay();
        }

    }

    public override void interact(GameObject initiator)
    {
        if (initiator.tag == "Player")
        {
            shaking=true;
        }
    }
    void FallAfterDelay()
    {
        
        shaking = false;
        this.gameObject.AddComponent<Rigidbody2D>();
    }
}
