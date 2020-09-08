
using System.Collections;
using System.Collections.Generic;
using System.Linq;
using UnityEngine;

public class ConveyorPlant : BasePlant
{
    private float speed = 50f;
    // Start is called before the first frame update
    private GameObject[] stemflower = new GameObject[2];
    void Start()
    {
        stemflower[0] = this.transform.Find("Stem").gameObject;
        stemflower[1] = this.transform.Find("Flower").gameObject;
        
        if (this.GetSpawner().GetComponent<ConveyorBeltCollisionInteractable>() != null)
        {
            speed = this.GetSpawner().GetComponent<ConveyorBeltCollisionInteractable>().getSpeed();
            Debug.Log(speed);
        }
        ee = GameObject.FindGameObjectWithTag("EventEmitter").GetComponent<EventEmitter>();
        

        
        this.GetSpawner().GetComponent<IInteractableWithEvents>()?.subsribeEvent("plant.falling", FallAfterDelay);
    }

    // Update is called once per frame
    void Update()
    {
        
        this.transform.position += Vector3.right * (1/speed);
        RaycastHit2D[] hits = Physics2D.RaycastAll(transform.position, -Vector2.up);
        var destroy = true;
        foreach(RaycastHit2D r in hits)
        {
            if (r.collider.gameObject == GetSpawner())
            {
                destroy = false;
            }
        }
        if (destroy) FallAfterDelay();
    }

    void FallAfterDelay(Object[] par)
    {
        this.FallAfterDelay();
    }
    void FallAfterDelay()
    {
         foreach(GameObject g in stemflower)
        {
            var r = g.AddComponent<Rigidbody2D>();
            r.AddForce(Random.insideUnitSphere * 2, ForceMode2D.Impulse);
        }
        Destroy(this.gameObject.GetComponent<BoxCollider2D>());
        Destroy(this.gameObject.GetComponentInChildren<BoxCollider2D>());
        Destroy(this.transform.Find("SecurityBox").gameObject);

    }
}
