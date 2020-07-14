using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class JumpPlant : MonoBehaviour
{
    [SerializeField] Vector2 pushStrength = Vector2.up;
    [SerializeField] bool keepBounciness = true;
    [SerializeField] Vector2 bounceStrength = Vector2.up;
    [SerializeField] float triggerVelocity = 1f;

    private GameObject player;
    private NormalPlant mainPlantScript;
    private PlayerOverPlant playerOverPlant;
    private bool readyToPush = false;

    void Start()
    {
        mainPlantScript = GetComponent<NormalPlant>();
        playerOverPlant = GetComponent<PlayerOverPlant>();
        player = GameObject.FindGameObjectWithTag("Player");
    }

    void Update()
    {
        if (!readyToPush)
        {
            //if the plant isn't ready, but gets ready with the current update, it can PUSH the player if he's standing on it or just landed
            if (UpdateReadyToPush() && (playerOverPlant.PlayerEntered() || playerOverPlant.PlayerOver())) PushTarget(player, pushStrength);
        } 
        else
        {
            //if the plant is ready (max height), it can BOUNCE the player if he's landing on it
            if(playerOverPlant.PlayerEntered()) PushTarget(player, bounceStrength);
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
        Rigidbody2D targetRB = target.GetComponent<Rigidbody2D>();
        //TODO: refactor to make it work with every pushable object
        MovementJoystick mj = target.GetComponent<MovementJoystick>();
        bool jumping = false;
        if (!mj || mj.IsJumping()) jumping = true;

        if (!jumping) 
        {
            mj.SetIsJumping(true);
            target.GetComponentInChildren<Animator>()?.SetTrigger("jumping");
            target.GetComponent<Rigidbody2D>()?.AddForce(strength, ForceMode2D.Impulse);
        }
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
