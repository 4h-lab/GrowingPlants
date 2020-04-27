using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class WaterSpeedTrigger : MonoBehaviour
{
    [SerializeField] private bool oneTime;
    //wether the trigger applies the opposite of speedMod if the player passes it in the opposite direction from the one intended
    [SerializeField] private bool reversible;
    [SerializeField] private float distanceFromPlayer;
    //wether the trigger activates when the player is farther than distanceFromPlayer, or closer
    [SerializeField] [Range(-1,1)] private int greater_lesser;
    [SerializeField] private float speedMod;
    //wether speedMod is added or assigned to risingSpeed
    [SerializeField] private bool relative;

    [SerializeField] private GameObject player;
    [SerializeField] private GameObject water;

    private float lastDist;

    private Water Water;

    // Start is called before the first frame update
    void Start()
    {
        //transform.localPosition = Vector2.zero;
        lastDist = player.transform.position.y - transform.position.y;
        if (greater_lesser == 0) greater_lesser = 1;
        Water = water.GetComponent<Water>();
    }

    private void Trigger()
    {
        string str = "Triggered!" + (player.transform.position.y - transform.position.y);
        Debug.Log(str);
        str = "speed is " + Water.risingSpeed;
        Debug.Log(str);
        if ((player.transform.position.y - transform.position.y - distanceFromPlayer) * greater_lesser > 0)
        {
            str = "entered ";
            Debug.Log(str);
            if (relative) Water.risingSpeed += speedMod;
            else Water.risingSpeed = speedMod;
            if (oneTime) this.gameObject.SetActive(false);
        }
        else if (reversible && relative && (player.transform.position.y - transform.position.y - distanceFromPlayer) * greater_lesser < 0)
        {
            str = "exited ";
            Debug.Log(str);
            Water.risingSpeed -= speedMod;
        }
        str = "speed is now " + Water.risingSpeed;
        Debug.Log(str);
    }

    // Update is called once per frame
    void Update()
    {
        Vector3 f = new Vector3(0, distanceFromPlayer, 0);
        Vector3 t = new Vector3(-100, 0, 0);
        Debug.DrawLine((transform.position + f), (transform.position + t), Color.red, 0.01f);
        float dd = (player.transform.position.y - transform.position.y - distanceFromPlayer) * (lastDist - distanceFromPlayer);
        //Debug.Log(dd);
        if (dd < 0) Trigger();
        if (player.transform.position.y - transform.position.y != 0) lastDist = player.transform.position.y - transform.position.y;
    }
}
