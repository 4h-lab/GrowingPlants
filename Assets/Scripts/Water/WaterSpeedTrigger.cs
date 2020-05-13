using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class WaterSpeedTrigger : MonoBehaviour
{

    public bool activated;

    [SerializeField]
    [Tooltip("USE ONLY IF thIS IS 'required' BY ANOTHER TRIGGER: if true, this trigger will not apply any speedmod when activated")]
    private bool triggerOnly;
    [SerializeField] [Tooltip("if true, deactivate the script once the Trigger is activated")]
    private bool oneTime;
    [SerializeField] [Tooltip("if true, the trigger applies the opposite of speedMod if the player passes it in the opposite direction from the one")]
    private bool reversible;
    [SerializeField] [Tooltip("how far from the GameObject sits the trigger treshold")]
    private float distanceFromPlayer;
    [SerializeField] [Tooltip("if -1, the trigger activates when the player gets closer than distanceFromPlayer, if 0 or 1, it activates when the player goes farther")]
    [Range(-1,1)] private int greater_lesser;
    [SerializeField] [Tooltip("the amount by which Water.risingSpeed is increased on activation")]
    private float speedMod;
    [SerializeField] [Tooltip("if true, speedMod is added or assigned to risingSpeed")]
    private bool relative;

    [SerializeField]
    [Tooltip("if assigned, this trigger will not activate unless the trigger in this field is also activated")]
    private GameObject required;
    [SerializeField] private GameObject player;
    [SerializeField] private GameObject water;

    private bool vertical;
    private float lastDist = 0.0f;

    private Water Water;

    // Start is called before the first frame update
    void Start()
    {
        activated = false;
        //transform.localPosition = Vector2.zero;
        if (greater_lesser == 0) greater_lesser = 1;
        Water = water.GetComponent<Water>();
        if (Mathf.Abs(Mathf.Sin(this.transform.rotation.z)) > 0.5f) vertical = true;
        else vertical = false;
        Trigger();
        lastDist = player.transform.position.y - transform.position.y;
    }

    private void Trigger()
    {
        if (triggerOnly) return;
        if (required != null && required.GetComponent<WaterSpeedTrigger>().activated == false) return;
        if (vertical)
        {
            if ((player.transform.position.y - transform.position.y - distanceFromPlayer) * greater_lesser > 0)
            {
                activated = true;
                if (relative) Water.risingSpeed += speedMod;
                else Water.risingSpeed = speedMod;
                if (oneTime) this.enabled = false;
            }
            else if (reversible && relative && (player.transform.position.y - transform.position.y - distanceFromPlayer) * greater_lesser < 0 && lastDist != 0.0f)
            {
                activated = false;
                Water.risingSpeed -= speedMod;
            }
        }
        else
        {
            if ((player.transform.position.x - transform.position.x - distanceFromPlayer) * greater_lesser > 0)
            {
                activated = true;
                if (relative) Water.risingSpeed += speedMod;
                else Water.risingSpeed = speedMod;
                if (oneTime) this.enabled = false;
            }
            else if (reversible && relative && (player.transform.position.x - transform.position.x - distanceFromPlayer) * greater_lesser < 0 && lastDist != 0.0f)
            {
                activated = false;
                Water.risingSpeed -= speedMod;
            }
        }
    }

    // Update is called once per frame
    void Update()
    {
        Vector3 f = new Vector3(0, distanceFromPlayer, 0);
        Vector3 t = new Vector3(-100, 0, 0);
        Debug.DrawLine((transform.position + f), (transform.position + t), Color.red, 0.01f);
        float dd = (player.transform.position.y - transform.position.y - distanceFromPlayer) * (lastDist - distanceFromPlayer);
        if (dd < 0) Trigger();
        if (vertical)
        {
            if (player.transform.position.y - transform.position.y != 0) lastDist = player.transform.position.y - transform.position.y;
        }
        else
        {
            if (player.transform.position.x - transform.position.x != 0) lastDist = player.transform.position.x - transform.position.x;
        }
    }
}
