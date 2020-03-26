using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CharacterSpawner : MonoBehaviour{
    private float radius;
    private float radius_x;

    private void Start()
    {
        radius= this.gameObject.GetComponent<BoxCollider2D>().bounds.extents.y*this.transform.localScale.y+(1f * this.transform.localScale.y);
        radius_x= this.gameObject.GetComponent<BoxCollider2D>().bounds.extents.x * this.transform.localScale.x;

    }
    public void requireSpawn() {

        //RaycastHit2D hit = Physics2D.Raycast(transform.position, -Vector2.up,radius); // fire a raycast directly down the player
        RaycastHit2D hit = Physics2D.CircleCast(transform.position, radius_x, -Vector2.up,radius-radius_x);



        if (hit.collider != null) {
            SpawnerTile st = hit.collider.gameObject.GetComponent<SpawnerTile>();
            if (st != null) { // check whether the object hit has a spawnertile component (that means, if it can spawn plants)
                st.spawn(gameObject); // invoke spawn passing the player as arg
            }
        }
    }


    /*private void OnDrawGizmos()
    {
        Gizmos.color = Color.red;
        
        Gizmos.DrawLine(this.transform.position, this.transform.position - new Vector3(0f, radius, 0f)) ;
    }*/

}
