using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using System;


public class PlantButton : Controls{

    private GameObject player;
    private GameObject child;

    void Start()
    {
        player = GameObject.FindGameObjectWithTag("Player");
        onControlsDisabled += onControlsDisabled_handler;
        onControlsEnabled += onControlsEnabled_handler;
        child = transform.GetChild(0).gameObject;

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
        player.GetComponent<CharacterSpawner>().spawnPlantRequest = true;
    }

    public void onControlsDisabled_handler(object obj, EventArgs e) {
        child.SetActive(false);
        
    }
    public void onControlsEnabled_handler(object obj, EventArgs e) {
        child.SetActive(true);
    }
}
