using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class GenericEventTrigger : MonoBehaviour
{
    [SerializeField] protected bool deactivateAfterNotify = true;
    protected bool triggered;

    public void Notify()
    {

    }

    public bool IsTriggered()
    {
        return triggered;
    }
}
