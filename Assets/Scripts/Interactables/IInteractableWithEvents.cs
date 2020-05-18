using System.Collections;
using System.Collections.Generic;
using UnityEngine;

/// <summary>
/// This interface represents an interactable that needs to provide an hook for events to be subsribed;
/// NOTE: remember to put an eventemitter in the implementing class!!!!!!
/// </summary>
public interface IInteractableWithEvents
{

    /// <summary>
    /// subsribe a new event to the class
    /// </summary>
    /// <param name="eventname"> the name of the event to attach the callback to</param>
    /// <param name="ev">the callback to be called when the event is fired</param>
    void subsribeEvent(string eventname, EventEmitter.EventCallback ev);


}
