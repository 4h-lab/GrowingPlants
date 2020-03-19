using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Passable : BaseContactInteractables
{
    public override void interact(GameObject initiator){
        if (initiator.transform.position.y <= transform.position.y) gameObject.GetComponent<EdgeCollider2D>().enabled = false;
        else gameObject.GetComponent<EdgeCollider2D>().enabled = true;
    }

    // Start is called before the first frame update
    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {
        
    }

}
