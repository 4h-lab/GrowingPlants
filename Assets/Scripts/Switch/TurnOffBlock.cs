using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class TurnOffBlock : BaseSwitchable
{
    public override void StartSwitchable()
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
        Color tmpColor = gameObject.GetComponent<SpriteRenderer>().color;
        ChangeColorPlaceholder(); //TEMP FUNCTION TO BE REMOVED WHEN SPRITES ARE IMPLEMENTED
        currentState = 0;
    }

    //TEMP FUNCTION TO BE REMOVED WHEN SPRITES ARE IMPLEMENTED
    private void ChangeColorPlaceholder()
    {
        Color tmpColor = gameObject.GetComponent<SpriteRenderer>().color;
        tmpColor.a = 0.5f;
        gameObject.GetComponent<SpriteRenderer>().color = tmpColor;
    }
}
