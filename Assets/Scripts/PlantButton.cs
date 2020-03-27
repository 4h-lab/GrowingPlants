using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlantButton : Controls{

    private GameObject player;

    void Start()
    {
        player = GameObject.FindGameObjectWithTag("Player");
    }

    void Update()
    {
        if (Input.GetKeyDown(KeyCode.Space) && enabled) SpawnPlant();
    }
    public override void OnPointerDown(UnityEngine.EventSystems.PointerEventData data)
    {
        if (enabled) SpawnPlant();
    }

    private void SpawnPlant()
    {
        player.GetComponent<CharacterSpawner>().requireSpawn();
    }
}
