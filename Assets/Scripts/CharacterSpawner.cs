using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CharacterSpawner : MonoBehaviour{
    public void requireSpawn() {
        RaycastHit2D hit = Physics2D.Raycast(transform.position, -Vector2.up); // fire a raycast directly down the player

     

        if (hit.collider != null) {
            SpawnerTile st = hit.collider.gameObject.GetComponent<SpawnerTile>();
            if (st != null) { // check whether the object hit has a spawnertile component (that means, if it can spawn plants)
                st.spawn(gameObject); // invoke spawn passing the player as arg
            }
        }
    }

}
