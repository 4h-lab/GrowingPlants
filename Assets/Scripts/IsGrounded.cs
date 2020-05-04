using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class IsGrounded : MonoBehaviour
{
    [SerializeField] private LayerMask ground;

    private bool grounded;

    public bool GetGrounded() { return grounded; }

    // Start is called before the first frame update
    void Start()
    {

    }

    private void OnCollisionEnter2D(Collision2D collision){
        if (collision?.contactCount > 0) {
            if (collision?.GetContact(0).normal.normalized == Vector2.up && ground == (ground | (1 << collision.gameObject.layer))) grounded = true;
        }
    }

    private void OnCollisionExit2D(Collision2D collision){
        if (collision?.contactCount > 0) {
            if (collision?.GetContact(0).normal.normalized == Vector2.up && ground == (ground | (1 << collision.gameObject.layer))) grounded = false;
        }
    }

    // Update is called once per frame
    void Update()
    {
        
    }
}
