using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class SwitchableSoil : MonoBehaviour, IInteractableWithEvents
{
    EventEmitter ee;

    void Start()
    {
        ee = gameObject.AddComponent(typeof(EventEmitter)) as EventEmitter;
    }

    public void DeactivateSoil()
    {
        ee.invoke("plant.falling", new Object[] { this });
    }

    public void subsribeEvent(string eventname, EventEmitter.EventCallback ev)
    {
        ee.on(eventname, ev);
    }
}
