using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using System;
using UnityEngine.EventSystems;

public class Controls : EventTrigger{
    protected bool enabled = true;

    public event EventHandler onControlsEnabled;
    public event EventHandler onControlsDisabled;

    public bool ControlsEnabled(bool enable){
        enabled = enable;
        if (enabled) onControlsEnabled?.Invoke(this, EventArgs.Empty);
        else onControlsDisabled?.Invoke(this, EventArgs.Empty);
        

        return enabled;
    }
}
