using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PhysicWater : MonoBehaviour
{
    public float risingSpeed;
    private bool cangoup = false;
    private float initY;
    private EventEmitter ee;


    private void triggerRisingLevel(Object[] p)
    {
        cangoup = true;
    }

    // Start is called before the first frame update
    void Start()
    {
        initY = this.transform.position.y;
        ee = GameObject.FindGameObjectWithTag("EventEmitter").GetComponent<EventEmitter>();
        ee.on("plant_created", triggerRisingLevel);
    }

    // Update is called once per frame
    void Update()
    {
        if (cangoup)
        {
            transform.localScale+=new Vector3(0f, Time.deltaTime * risingSpeed * GameManager.customTimeScale,0f);
        }
    }
}


