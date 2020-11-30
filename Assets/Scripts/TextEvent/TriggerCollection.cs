using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class TriggerCollection : GenericEventTrigger
{
    [Tooltip("this trigger is true only when every trigger on the list is")]
    [SerializeField] List<GenericEventTrigger> triggerList;

    void Start()
    {
        triggered = false;
    }

    void LateUpdate()
    {
        if (CheckIfTriggered()) Notify();
    }

    bool CheckIfTriggered()
    {
        bool partialTriggerCheck = true;
        if (triggered && deactivateAfterNotify) return false;
        foreach(GenericEventTrigger triggerEvent in triggerList)
        {
            if (!triggerEvent.IsTriggered())
            {
                partialTriggerCheck = false;
                break;
            }
        }
        return partialTriggerCheck;
    }
}
