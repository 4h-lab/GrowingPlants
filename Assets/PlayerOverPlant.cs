using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerOverPlant : MonoBehaviour
{
    private bool playerEnter;
    private bool playerStay;
    private bool playerExit;

    void Start()
    {
        
    }

    private void OnCollisionEnter2D(Collision2D collision)
    {
        if (collision.gameObject.tag != "Player") return;
        if (collision.contacts[0].normal != Vector2.down) return;
        playerEnter = true;
        playerStay = false;
        playerExit = false;
    }

    private void OnCollisionStay2D(Collision2D collision)
    {
        if (collision.gameObject.tag != "Player") return;
        playerEnter = false;
        playerStay = true;
        playerExit = false;
    }

    private void OnCollisionExit2D(Collision2D collision)
    {
        if (collision.gameObject.tag != "Player") return;
        playerEnter = false;
        playerStay = false;
        playerExit = true;
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
