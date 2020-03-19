using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Passable : BaseContactInteractables
{
    public override void interact(GameObject initiator){
        gameObject.GetComponent<EdgeCollider2D>().enabled = (initiator.transform.position.y > transform.position.y);

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
