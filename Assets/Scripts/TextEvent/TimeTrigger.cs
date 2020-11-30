using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class TimeTrigger : GenericEventTrigger
{
    [Tooltip("After how many seconds the trigger is active")]
    [SerializeField] float triggerTime;
    [Tooltip("Set to true to make the trigger active Before triggerTime")]
    [SerializeField] bool beforeTrigger = false;
    [SerializeField] float repeatDelay = 0;
    float initTime;
    float currentDelay = 0;

    void Start()
    {
        initTime = Time.realtimeSinceStartup;
        triggered = beforeTrigger;  //se dev'essere attivo prima di timeTrigger inizia true, altrimenti false
    }

    void Update()
    {
        if (CheckIfTriggered()) Notify();
    }

    bool CheckIfTriggered()
    {
        if (triggered) {
            if (!deactivateAfterNotify) {
                currentDelay = (currentDelay + 1) % repeatDelay;
                if (currentDelay == 0) return true;
            }
            return false;
        }
        if(!beforeTrigger) triggered = Time.time - initTime >= triggerTime;
        else triggered = Time.time - initTime <= triggerTime;
        return triggered;
    }
}
