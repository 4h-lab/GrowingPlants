using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class JumpPlant : MonoBehaviour
{
    [Tooltip("Strength of the force added to the player if he's on the plant when it reaches its max height")]
    [SerializeField] Vector2 pushStrength = Vector2.up;
    [Tooltip("Makes the plant bounce the player if he fall on it after the initial push frame")]
    [SerializeField] bool keepBounciness = true;
    [Tooltip("Strength of the force added to the player if he lands on the plant after the initial push frame (only if keepBounciness is true)")]
    [SerializeField] Vector2 bounceStrength = Vector2.up;
    [Tooltip("Minimum falling speed to trigger the bounce (only if keepBounciness is true)")]
    [SerializeField] float triggerVelocity = 1f;

    private GameObject player;
    private NormalPlant mainPlantScript;
    private PlayerOverPlant playerOverPlant;
    private bool JumpActive = false;

    void Start()
    {
        mainPlantScript = GetComponent<NormalPlant>();
        playerOverPlant = GetComponent<PlayerOverPlant>();
        player = GameObject.FindGameObjectWithTag("Player");
    }

    void Update()
    {
        if (!JumpActive)
        {
            //the plant can push only during the frame when the push and bounce status becomes true (see CanPush conditions)
            if (CanPush()) PushTarget(player, pushStrength);
        }
        else
        {
            if (CanBounce()) PushTarget(player, bounceStrength);
        }
    }

    /*
     * updates the status of the plant, which is "active" from the moment it reaches its max height onward
     * also returns true during the frame when the status is changed, false otherwise
     */
    private bool UpdateJumpActive()
    {
        if (JumpActive) return false;
        if (transform.position.y >= mainPlantScript.GetInitY() + mainPlantScript.GetMaxHeigth())
        {
            JumpActive = true;
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
            target.GetComponent<Rigidbody2D>()?.AddForce(strength, ForceMode2D.Impulse);
        }
    }

    /*
     * the plant can push the player only if he has just landed on it or is staying on it
     * when UpdateReadyToPush returns true, which is only during the frame when the plant
     * reaches its max height
     */
    private bool CanPush()
    {
        return UpdateJumpActive()
            && (playerOverPlant.PlayerEntered() || playerOverPlant.PlayerOver());
    }

    /*
     * the plant can bounce the player only after having reached its max height
     * (this is checked by Update) and only if the bounce mechanic is active and
     * the player is moving toward it fast enough
     */
    private bool CanBounce()
    {
        return keepBounciness
            && playerOverPlant.PlayerEntered()
            && player.GetComponent<Rigidbody2D>().velocity.y < triggerVelocity;
    }
}
