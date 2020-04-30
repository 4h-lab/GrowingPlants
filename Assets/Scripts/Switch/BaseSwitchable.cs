using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public abstract class BaseSwitchable : MonoBehaviour
{
    [SerializeField] protected int startingState = 0;
    [SerializeField] protected int currentState;

    void Start()
    {
        StartSwitchable();
        currentState = startingState;
    }

    public abstract void StartSwitchable();

    public abstract int ChangeState(BaseSwitcher switcher, string action);
}
