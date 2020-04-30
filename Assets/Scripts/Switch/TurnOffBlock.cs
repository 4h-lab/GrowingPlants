using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class TurnOffBlock : BaseSwitchable
{
    public override void StartSwitchable()
    {

    }

    void Update()
    {
        
    }

    public override int ChangeState(BaseSwitcher switcher, string action)
    {
        return ChangeState(switcher);
    }

    public int ChangeState(BaseSwitcher switcher)
    {
        if (currentState == 1) TurnOff();
        return currentState;
    }

    public void TurnOff()
    {
        gameObject.GetComponent<SpriteRenderer>().color = Color.white;
        gameObject.GetComponent<BoxCollider2D>().enabled = false;
        currentState = 0;
    }
}
