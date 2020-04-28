using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class BaseSwitcher : MonoBehaviour
{
    void Start()
    {
        
    }

    void Update()
    {
        
    }

    public int ChangeSwitchableState(BaseSwitchable switchable, string action)
    {
        return switchable.ChangeState(this, action);
    }
}
