using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public abstract class BaseCollisionInteractable : BaseInteractable
{
    // Start is called before the first frame update
    private void OnCollisionEnter2D(Collision2D collision)
    {
        GameObject go = collision.gameObject;
        interact(go);
    }
}
