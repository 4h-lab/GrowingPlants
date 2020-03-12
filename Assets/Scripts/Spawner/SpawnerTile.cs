using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class SpawnerTile : MonoBehaviour{
    public GameObject spawnableObject;

    public void spawn(GameObject initiator) {
        //GameObject.Instantiate(spawnableObject, new Vector3(initiator.transform.position.x, transform.position.y, transform.position.z), Quaternion.identity);

        float yPos = GetComponent<Collider2D>().bounds.max.y - spawnableObject.GetComponent<Collider2D>().bounds.extents.y;
        Debug.Log("yPos: " + yPos + "   " + GetComponent<Collider2D>().bounds.max.y);

        GameObject.Instantiate(spawnableObject, new Vector3(initiator.transform.position.x, yPos, transform.position.z), Quaternion.identity);
    }

}
