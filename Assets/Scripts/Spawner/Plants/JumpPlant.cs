using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class JumpPlant : MonoBehaviour
{
    [SerializeField] Vector2 pushStrength = Vector2.up;
    [SerializeField] bool keepBounciness = true;
    [SerializeField] Vector2 bounceStrength = Vector2.up;
    [SerializeField] float triggerVelocity = 1f;

    private Animator anim;
    private NormalPlant mainPlantScript;
    private bool readyToPush = false;

    void Start()
    {
        mainPlantScript = GetComponent<NormalPlant>();
    }

    void Update()
    {
        if (!readyToPush && UpdateReadyToPush())
        {
            GameObject player = mainPlantScript.GetPlayerOnPlant();
            anim = player.GetComponentInChildren<Animator>();
            if (player) PushTarget(player, pushStrength);
        }
    }

    // Set readyToPush to true when the max heigth has just been reached, and return true in that frame, False otherwise
    private bool UpdateReadyToPush()
    {
        if (readyToPush) return false;
        if (transform.position.y >= mainPlantScript.GetInitY() + mainPlantScript.GetMaxHeigth())
        {
            readyToPush = true;
            return true;
        }
        return false;
    }

    private void PushTarget(GameObject target, Vector2 strength)
    {
        target.GetComponent<Rigidbody2D>().AddForce(strength, ForceMode2D.Impulse);
        anim.SetTrigger("jumping");
    }

    private void OnCollisionEnter2D(Collision2D collision)
    {
        if (CanBounce(collision)) PushTarget(collision.gameObject, bounceStrength);
    }

    private bool CanBounce(Collision2D target)
    {
        return target.gameObject.tag == "Player"
            && target.GetContact(0).relativeVelocity.y < -triggerVelocity
            && keepBounciness
            && readyToPush;
    }
}
