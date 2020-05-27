using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class IsGrounded : MonoBehaviour
{
    [SerializeField] private float groundedTreshold;
    [SerializeField] private LayerMask ground;

    private bool grounded;
    private Animator anim;
    private Collider2D _collider;

    public bool GetGrounded() { return grounded; }

    // Start is called before the first frame update
    void Start() {
        anim = GetComponentInChildren<Animator>();
        _collider = GetComponent<Collider2D>();
        StartCoroutine(check());
    }

    private void OnCollisionEnter2D(Collision2D collision) {
        if (collision?.contactCount > 0) {
            if (
                collision?.GetContact(0).normal.normalized == Vector2.up &&
                this.gameObject.transform.position.y - collision?.GetContact(0).point.y > groundedTreshold &&
                ground == (ground | (1 << collision.gameObject.layer))
            ) {
                grounded = true;
                Debug.Log("GROUNDED: " + grounded.ToString());
                if (anim != null) anim.SetBool("Falling", false);
            }
        }
    }

    private void OnCollisionExit2D(Collision2D collision) {
        if (collision?.contactCount > 0) {
            if (
                collision?.GetContact(0).normal.normalized == Vector2.up &&
                (this.gameObject.transform.position.y - collision?.GetContact(0).point.y) < groundedTreshold &&
                ground == (ground | (1 << collision.gameObject.layer))
            ) {
                grounded = false;
                Debug.Log("GROUNDED: " + grounded.ToString());
                if (anim != null) anim.SetBool("Falling", true);
            }
        }
    }

    // Update is called once per frame
    void Update() {

    }

    private IEnumerator check() {
        while (true) {
            grounded = checkGroundLevel();
            if (anim != null) anim.SetBool("Falling", !grounded);
            yield return new WaitForSeconds(.05f); 
        }
    }

    private bool checkGroundLevel() {
        RaycastHit2D[] hits = Physics2D.BoxCastAll(_collider.bounds.center, _collider.bounds.extents * 2, 0f, Vector2.down, _collider.bounds.extents.y + .01f, ground);
        return (hits.Length >= 1); // return true if there was at least 1 hit
    }

}
