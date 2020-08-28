using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class TeleportPlant : MonoBehaviour
{

    private GameObject player;
    private NormalPlant mainPlantScript;
    private PlayerOverPlant playerOverPlant;

    private Target target;
    private GameObject targetObj;

    void Start()
    {
        mainPlantScript = GetComponent<NormalPlant>();
        playerOverPlant = GetComponent<PlayerOverPlant>();
        player = GameObject.FindGameObjectWithTag("Player");

        target = player.GetComponentInChildren<Target>();
        targetObj = target.gameObject;

        Teleport(player, targetObj.transform.position);
        targetObj.SetActive(false);
        //Vector2 dest = player.transform.position;
        //dest.x += 10;
        //if(DestinationOk()) Teleport(player, dest);
    }

    void Update()
    {
        
    }

    private bool DestinationOk()
    {
        //cast
        return true;
    }

    private void Teleport(GameObject target, Vector2 destination)
    {
        target.transform.position = destination;
    }
}
