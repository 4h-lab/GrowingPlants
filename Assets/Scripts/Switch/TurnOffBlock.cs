using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class TurnOffBlock : BaseSwitchable
{
    //TODO: remove when we have sprites
    [SerializeField] private float deactivatedAlphaPlaceholder = 0f;

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
        GetComponent<SwitchableSoil>()?.DeactivateSoil();
        gameObject.GetComponent<BoxCollider2D>().enabled = false;
        Destroy(gameObject.GetComponent<Rigidbody2D>());
        ChangeColorPlaceholder(); //TEMP FUNCTION TO BE REMOVED WHEN SPRITES ARE IMPLEMENTED
        currentState = 0;
    }

    //TEMP FUNCTION TO BE REMOVED WHEN SPRITES ARE IMPLEMENTED
    private void ChangeColorPlaceholder()
    {
        Color tmpColor = gameObject.GetComponent<SpriteRenderer>().color;
        tmpColor.a = deactivatedAlphaPlaceholder;
        gameObject.GetComponent<SpriteRenderer>().color = tmpColor;
    }
}
