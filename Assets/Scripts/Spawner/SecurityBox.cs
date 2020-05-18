using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class SecurityBox : MonoBehaviour
{
    // Start is called before the first frame update
    Collider2D collider;
    Bounds bounds;
    Bounds playerBounds;
    Transform player;
    float shellDistance = 0.1f;
    PlatformEffector2D platform;

    /*
    * TEMP
    * Used to prevent the plant from pressing button during the first frames after it's creation
    * Check if there's a way to link this functionality to the animation state when we have some
    * animated stuff going.
    * 
    * Other TEMP code related to this:
    * - initializationTime in Start()
    * - CanPressSwitches method at the end of this script
    */
    private float initializationTime;
    [SerializeField] private float timeBeforeInteraction = 0.5f;

    void Start()
    {
        //TEMP - see above
        initializationTime = Time.timeSinceLevelLoad;

        collider = this.GetComponent<Collider2D>();

        bounds = this.GetComponent<Collider2D>().bounds;

        player = GameObject.FindGameObjectWithTag("Player").transform;
        playerBounds = GameObject.FindGameObjectWithTag("Player").GetComponent<Collider2D>().bounds;
        platform = this.GetComponent<PlatformEffector2D>();
    }

    private void Update()
    {
        var playerPos = player.position.y - (playerBounds.extents.y);
        var thisPos = this.transform.position.y + bounds.extents.y;
        /*if (playerPos >= thisPos)
        {
            platform.surfaceArc = 180f;
        }
        else
        {
            platform.surfaceArc = 150f;
        }*/
        if (playerPos >= thisPos)
        {
            /*Debug.Log(playerPos + "<=" + thisPos);
            Debug.DrawLine(player.position, playerPos * Vector3.up);
            Debug.DrawLine(this.transform.position, Vector3.up * thisPos);*/
            //collider.enabled = true;
            collider.isTrigger = false;
        }
        else
        {
            collider.isTrigger = true;
            //collider.enabled = false;
        }
    }

    //TEMP - see initializationTime notes
    public bool CanPressSwitches()
    {
        return Time.timeSinceLevelLoad - initializationTime > timeBeforeInteraction;
    }
}
