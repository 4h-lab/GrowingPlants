using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public abstract class BaseSwitchable : MonoBehaviour
{
    [SerializeField] private int startingState = 0;
    [SerializeField] private int currentState;

    void Start()
    {
        StartSwitchable();
        currentState = startingState;
    }

    public abstract void StartSwitchable();

    public abstract int ChangeState(BaseSwitcher switcher, string action);
}
