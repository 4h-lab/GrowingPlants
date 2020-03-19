using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlantContactInteractable : BaseContactInteractables
{
    private EventEmitter ee;

    private void Start()
    {
        ee = GameObject.FindGameObjectWithTag("EventEmitter").GetComponent<EventEmitter>();
    }
    public override void interact(GameObject initiator)
    {
        if (initiator.GetComponent<CharacterSpawner>()!=null)
        {
            return;
        }
        this.transform.parent.GetComponent<NormalPlant>().hitStuff();
    }
}
