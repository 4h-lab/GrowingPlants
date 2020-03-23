using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerWetInteractable : BaseContactInteractables
{
    public override void interact(GameObject initiator)
    {
        if (initiator.GetComponent<PhysicWater>() != null) GameObject.FindGameObjectWithTag("GameManager").GetComponent<GameManager>().notifyOfNewSomething("water.touchedby");
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
