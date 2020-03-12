using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class SpawnerTile : MonoBehaviour{
    public GameObject spawnableObject;

    public void spawn(GameObject initiator) {
        GameObject.Instantiate(spawnableObject, initiator.transform);
    }

}
