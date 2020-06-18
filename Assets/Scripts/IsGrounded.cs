using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class IsGrounded : MonoBehaviour
{
    [SerializeField] private float groundedTreshold;
    [SerializeField] private LayerMask groundLayerMask;

    private bool grounded;
    private Animator anim;
    private Collider2D _collider;

    private Rigidbody2D rb;




    public bool GetGrounded() { return grounded; }

    // Start is called before the first frame update
    void Start() {
        rb = GetComponent<Rigidbody2D>();
        anim = GetComponentInChildren<Animator>();
        groundLayerMask = (1 << 0) | (1 << LayerMask.NameToLayer("plant")) | (1 << LayerMask.NameToLayer("onewayplatform"));
        _collider = GetComponent<Collider2D>();
        StartCoroutine(check());
    }
    
    private IEnumerator check() {
        while (true) {
            bool prev = grounded;
            Collider2D[] da_hits;
            grounded = checkGroundLevel(out da_hits);
            if (grounded && !prev && !this.GetComponent<MovementJoystick>().getSquished()) { // first frame in which you touched the floor
                GameObject.FindGameObjectWithTag("playerps_dustfalling").GetComponent<ParticleSystem>()?.Play();
                if (da_hits != null) {
                    foreach (Collider2D c in da_hits) {
                        c.gameObject.GetComponent<IFallInteractable>()?.fallInteract(this.gameObject);
                    }
                }
            }
            if (anim != null) anim.SetBool("Falling", !grounded && (rb.velocity.y < 0.01f));
            yield return new WaitForSeconds(.05f); 
        }
    }


    private bool checkGroundLevel(out Collider2D[] da_hits) {
        //RaycastHit2D[] hits = Physics2D.BoxCastAll(new Vector2(_collider.bounds.center.x, _collider.bounds.center.y + _collider.bounds.extents.y), _collider.bounds.extents * 2, 0f, Vector2.down, _collider.bounds.extents.y + .01f, ground);
        RaycastHit2D[] hits = Physics2D.LinecastAll(new Vector2(_collider.bounds.center.x - _collider.bounds.extents.x, 
                                                                _collider.bounds.center.y - _collider.bounds.extents.y - .025f),
                                                    new Vector2(_collider.bounds.center.x + _collider.bounds.extents.x,
                                                                _collider.bounds.center.y - _collider.bounds.extents.y - .025f), groundLayerMask);
        Debug.DrawLine(new Vector2(_collider.bounds.center.x - _collider.bounds.extents.x,
                                                                _collider.bounds.center.y - _collider.bounds.extents.y - .025f),
                                                    new Vector2(_collider.bounds.center.x + _collider.bounds.extents.x,
                                                                _collider.bounds.center.y - _collider.bounds.extents.y - .025f), Color.red);

        if (hits.Length > 0) {
            da_hits = new Collider2D[hits.Length];
            for (int i = 0; i < hits.Length; i++) {
                da_hits[i] = hits[i].collider;
            }
            return true;
        } else {
            da_hits = null;
            return false;
        }
    }

}
