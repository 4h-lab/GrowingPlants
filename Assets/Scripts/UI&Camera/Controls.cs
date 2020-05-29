using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.EventSystems;

public class Controls : EventTrigger
{
    protected bool enabled = true;

    public bool ControlsEnabled(bool enable)
    {
        enabled = enable;
        return enabled;
    }
}
