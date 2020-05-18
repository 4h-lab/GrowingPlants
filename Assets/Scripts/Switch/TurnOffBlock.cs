using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class TurnOffBlock : BaseSwitchable
{
    //TODO: remove when we have sprites
    [Header("Placeholder")]
    [SerializeField] private float deactivatedAlpha = 0f;
    [SerializeField] private float fadingTime = 1f;
    private float currentAlpha = 1;

    //TEMP FUNCTION TO BE REMOVED WHEN SPRITES ARE IMPLEMENTED
    void Update()
    {
        if (currentState == 0 && currentAlpha > deactivatedAlpha) ChangeColorPlaceholder();
    }

    public override void StartSwitchable()
    {
        //TEMP FUNCTION TO BE REMOVED WHEN SPRITES ARE IMPLEMENTED
        currentAlpha = gameObject.GetComponent<SpriteRenderer>().color.a;
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
        currentState = 0;
    }

    //TEMP FUNCTION TO BE REMOVED WHEN SPRITES ARE IMPLEMENTED
    private void ChangeColorPlaceholder()
    {
        Color tmpColor = gameObject.GetComponent<SpriteRenderer>().color;
        tmpColor.a = tmpColor.a - (tmpColor.a - deactivatedAlpha)/fadingTime * Time.deltaTime;
        gameObject.GetComponent<SpriteRenderer>().color = tmpColor;
    }
}
