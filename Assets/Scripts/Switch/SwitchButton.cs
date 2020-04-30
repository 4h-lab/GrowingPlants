using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class SwitchButton : BaseSwitcher
{
    [SerializeField] string switchAction = "UNUSED";
    [SerializeField] string[] pressedBy;

    void Start()
    {
        
    }

    void Update()
    {
        
    }

    public void OnTriggerEnter2D(Collider2D collision)
    {
        Debug.Log("COLLISION");
        if (pressFilter(collision.gameObject.tag))
        {
            Debug.Log("OK");
            for (int i = 0; i < connected_switchables.Length; i++)
            {
                BaseSwitchable switchable = connected_switchables[i].GetComponent<BaseSwitchable>();
                if (switchable != null)
                {
                    switchable.ChangeState(this, switchAction);
                }
            }
        }
    }

    //checks if an object tag is contained in the pressedBy array
    //used to check if the button has collided with something which can press it
    private bool pressFilter(string collisionTag)
    {
        for (int i = 0; i < pressedBy.Length; i++) if (collisionTag.Equals(pressedBy[i])) return true;
        return false;
    }
}
