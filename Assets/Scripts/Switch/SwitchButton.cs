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
        if (pressFilter(collision))  //for plants, the Tag "Plant" is on the securityBox child
        {
            for (int i = 0; i < connectedSwitchables.Length; i++)
            {
                connectedSwitchables[i].GetComponent<BaseSwitchable>()?.ChangeState(this, switchAction);
            }
            currentState = 1;
            ChangeColorPlaceholder(); //TEMP FUNCTION TO BE REMOVED WHEN SPRITES ARE IMPLEMENTED
        }
    }

    //checks if an object tag is contained in the pressedBy array
    //used to check if the button has collided with something which can press it
    private bool pressFilter(Collider2D collision)
    {
        //TEMP - see below
        if (InteractionWithJustSpawnedPlant(collision)) return false;

        for (int i = 0; i < pressedBy.Length; i++) if (collision.gameObject.tag.Equals(pressedBy[i])) return true;
        return false;
    }

    //TEMP - refactor this when we can link this functionality to animations
    //returns true if collision is a plant with too little instanced time to press button, false otherwise
    private bool InteractionWithJustSpawnedPlant(Collider2D collision)
    {
        if (!collision.gameObject.tag.Equals("Plant")) return false;
        if (collision.gameObject.GetComponent<SecurityBox>().CanPressSwitches()) return false;
        return true;
    }

    //TEMP FUNCTION TO BE REMOVED WHEN SPRITES ARE IMPLEMENTED
    private void ChangeColorPlaceholder()
    {
        Color tmpColor = gameObject.GetComponent<SpriteRenderer>().color;
        tmpColor.a = 0.5f;
        gameObject.GetComponent<SpriteRenderer>().color = tmpColor;
    }
}
