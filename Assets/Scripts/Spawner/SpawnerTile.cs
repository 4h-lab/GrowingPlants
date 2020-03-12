using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class SpawnerTile : MonoBehaviour{
    public GameObject spawnableObject;

    public void spawn(GameObject initiator) {
        //GameObject.Instantiate(spawnableObject, new Vector3(initiator.transform.position.x, transform.position.y, transform.position.z), Quaternion.identity);
        GameObject x = GameObject.Instantiate(spawnableObject, new Vector3(initiator.transform.position.x, GetComponent<Collider2D>().bounds.max.y, transform.position.z), Quaternion.identity);
    }

}
