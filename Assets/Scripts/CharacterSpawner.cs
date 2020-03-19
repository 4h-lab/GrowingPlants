using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CharacterSpawner : MonoBehaviour{
    private float radius;

    private void Start()
    {
        radius= this.gameObject.GetComponent<SpriteRenderer>().sprite.bounds.extents.y*this.transform.localScale.y;
        //var vect = this.transform.position - new Vector3(0f, t, 0f);
        //radius = vect.y;
        Debug.Log(this.gameObject.GetComponent<SpriteRenderer>().sprite.bounds.size);
    }
    public void requireSpawn() {
        RaycastHit2D hit = Physics2D.Raycast(transform.position, -Vector2.up,radius); // fire a raycast directly down the player

     

        if (hit.collider != null) {
            SpawnerTile st = hit.collider.gameObject.GetComponent<SpawnerTile>();
            if (st != null) { // check whether the object hit has a spawnertile component (that means, if it can spawn plants)
                st.spawn(gameObject); // invoke spawn passing the player as arg
            }
        }
    }


    private void OnDrawGizmos()
    {
        Gizmos.color = Color.red;
        
        Gizmos.DrawLine(this.transform.position, this.transform.position - new Vector3(0f, radius, 0f)) ;
    }

}
