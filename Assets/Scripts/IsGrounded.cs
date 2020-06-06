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
    
    private IEnumerator check() {
        while (true) {
            bool prev = grounded;
            grounded = checkGroundLevel();
            if (grounded && !prev) { // first frame in which you touched the floor
                GameObject.FindGameObjectWithTag("playerps_dustfalling").GetComponent<ParticleSystem>()?.Play();
            }
            if (anim != null) anim.SetBool("Falling", !grounded);
            yield return new WaitForSeconds(.05f); 
        }
    }


    private bool checkGroundLevel() {
        //RaycastHit2D[] hits = Physics2D.BoxCastAll(new Vector2(_collider.bounds.center.x, _collider.bounds.center.y + _collider.bounds.extents.y), _collider.bounds.extents * 2, 0f, Vector2.down, _collider.bounds.extents.y + .01f, ground);
        RaycastHit2D[] hits = Physics2D.LinecastAll(new Vector2(_collider.bounds.center.x - _collider.bounds.extents.x, 
                                                                _collider.bounds.center.y - _collider.bounds.extents.y - .025f),
                                                    new Vector2(_collider.bounds.center.x + _collider.bounds.extents.x,
                                                                _collider.bounds.center.y - _collider.bounds.extents.y - .025f));
        Debug.DrawLine(new Vector2(_collider.bounds.center.x - _collider.bounds.extents.x,
                                                                _collider.bounds.center.y - _collider.bounds.extents.y - .025f),
                                                    new Vector2(_collider.bounds.center.x + _collider.bounds.extents.x,
                                                                _collider.bounds.center.y - _collider.bounds.extents.y - .025f), Color.red);

        //PopupText.createNewPopup(transform.position, hits.Length.ToString(), Color.white);
        return (hits.Length >= 1); // return true if there was at least 1 hit
    }

}
