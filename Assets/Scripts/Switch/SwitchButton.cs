using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class SwitchButton : BaseSwitcher
{
    [SerializeField] string[] pressedBy;
    [SerializeField] string switchAction = "UNUSED";

    public override void StartSwitcher() { }

    public void OnTriggerEnter2D(Collider2D collision)
    {
        if (currentState == 0) PressButton(collision);
    }

    public void PressButton(Collider2D collision)
    {
        if (pressFilter(collision.gameObject.tag))
        {
            for (int i = 0; i < connected_switchables.Length; i++)
            {
                BaseSwitchable switchable = connected_switchables[i].GetComponent<BaseSwitchable>();
                if (switchable != null)
                {
                    switchable.ChangeState(this, switchAction);
                }
            }
        }
        currentState = 1;
        ChangeColorPlaceholder(); //TEMP FUNCTION TO BE REMOVED WHEN SPRITES ARE IMPLEMENTED
    }

    //checks if an object tag is contained in the pressedBy array
    //used to check if the button has collided with something which can press it
    private bool pressFilter(string collisionTag)
    {
        for (int i = 0; i < pressedBy.Length; i++) if (collisionTag.Equals(pressedBy[i])) return true;
        return false;
    }

    //TEMP FUNCTION TO BE REMOVED WHEN SPRITES ARE IMPLEMENTED
    private void ChangeColorPlaceholder()
    {
        Color tmpColor = gameObject.GetComponent<SpriteRenderer>().color;
        tmpColor.a = 0.5f;
        gameObject.GetComponent<SpriteRenderer>().color = tmpColor;
    }
}
