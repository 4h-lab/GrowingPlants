using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerOverPlant : MonoBehaviour
{
    private bool playerEnter;
    private bool playerStay;
    private bool playerExit;

    private bool previousEnter;
    private bool previousExit;

    void Start()
    {
        
    }

    void LateUpdate()
    {
        if (playerEnter) playerEnter = !playerEnter;
        if (playerExit) playerExit = !playerExit;
    }

    private void OnCollisionEnter2D(Collision2D collision)
    {
        if (collision.gameObject.tag != "Player") return;
        playerEnter = true;
        previousEnter = true;
    }

    private void OnCollisionStay2D(Collision2D collision)
    {
        if (collision.gameObject.tag != "Player") return;
        playerStay = true;
    }

    private void OnCollisionExit2D(Collision2D collision)
    {
        if (collision.gameObject.tag != "Player") return;
        playerStay = false;
        playerExit = true;
        previousExit = true;
    }

    public bool PlayerEntered()
    {
        return playerEnter;
    }

    public bool PlayerOver()
    {
        return playerStay;
    }

    public bool PlayerExited()
    {
        return playerExit;
    }
}
