using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using System.Linq;

public class CharacterSpawner : MonoBehaviour{
    
    private Rigidbody2D rgb2d;
    protected ContactFilter2D contactFilter;
    protected RaycastHit2D[] hitBuffer = new RaycastHit2D[16];
    private float shellRadius=0.01f;

    [SerializeField]
    private float spawnOffset;

    private void Start()
    {
        
        contactFilter.useTriggers = false;
        var lm = Physics2D.GetLayerCollisionMask(gameObject.layer);
        lm |= (1 << LayerMask.NameToLayer("plant"));
        contactFilter.SetLayerMask(lm);
        rgb2d = GetComponent<Rigidbody2D>();

    }

    public void requireSpawn() {


        //RaycastHit2D hit = Physics2D.Raycast(transform.position, -Vector2.up,radius); // fire a raycast directly down the player
        int count = rgb2d.Cast(-Vector2.up, contactFilter, hitBuffer, shellRadius);
        var d = new Dictionary<RaycastHit2D,float>();
        bool soilBelow = false;
        if (count > 0) { 
            for (int i = 0; i < count; i++)
            {
                d.Add(hitBuffer[i], hitBuffer[i].distance);
            }
            d.OrderBy(item => item.Value);
            //var hit = d.First().Key;
            foreach (var hitData in d)
            {
                var hit = hitData.Key;
                float soilUpperBound = hit.collider.gameObject.GetComponent<Collider2D>().bounds.center.y + hit.collider.gameObject.GetComponent<Collider2D>().bounds.extents.y;
                float playerLowerBound = GetComponent<BoxCollider2D>().bounds.center.y - GetComponent<BoxCollider2D>().bounds.extents.y;
                
                if (hit.collider != null && soilUpperBound <= playerLowerBound)
                {
                    SpawnerTile st = hit.collider.gameObject.GetComponent<SpawnerTile>();
                    Vector3 pt = hit.point - Vector2.up * spawnOffset;
                    if (st != null)
                    { // check whether the object hit has a spawnertile component (that means, if it can spawn plants)
                        st.spawnHere(gameObject, pt); // invoke spawn passing the player as arg
                        break;
                    }
                }
            }
        }
    }


    private void OnDrawGizmos()
    {
        Gizmos.color = Color.red;

    }

    

}
