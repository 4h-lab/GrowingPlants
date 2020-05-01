using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public abstract class BaseSwitcher : MonoBehaviour
{
    [SerializeField] protected int startingState = 0;
    [SerializeField] protected int currentState;
    [SerializeField] protected GameObject[] connected_switchables;

    void Start()
    {
        StartSwitcher();
        currentState = startingState;
    }

    public abstract void StartSwitcher();

    public int ChangeSwitchableState(BaseSwitchable switchable, string action)
    {
        return switchable.ChangeState(this, action);
    }
}
