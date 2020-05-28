using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using System.Linq;

public class CharacterSpawner : MonoBehaviour{

    public bool spawnPlantRequest = false;

    [SerializeField] private int bufferFrames;
    [SerializeField] private float shellRadius = 0.01f;
    [SerializeField] private float spawnOffset;

    private int curBufferFrames;
    private Rigidbody2D rgb2d;

    protected ContactFilter2D contactFilter;
    protected RaycastHit2D[] hitBuffer = new RaycastHit2D[16];

    private void Start()
    {
        contactFilter.useTriggers = false;
        var lm = Physics2D.GetLayerCollisionMask(gameObject.layer);
        lm |= (1 << LayerMask.NameToLayer("plant"));
        contactFilter.SetLayerMask(lm);
        rgb2d = GetComponent<Rigidbody2D>();
        spawnPlantRequest = false;
        curBufferFrames = 0;
    }

    private bool Buffer()
    {
        if (spawnPlantRequest)//if an input to spawn a plant has been buffered
        {
            if (requireSpawn())//if the spawning was successful remove bufferd input
            {
                spawnPlantRequest = false;
                curBufferFrames = 0;
                return true;
            }
            else curBufferFrames += 1;//if the spawning was not successful, keep waiting
            if (curBufferFrames >= bufferFrames) spawnPlantRequest = false;//if there hasn't been a successful spawn within buffertime, remove buffered input
        }
        return false;
    }

    public bool requireSpawn() {
        int count = rgb2d.Cast(-Vector2.up, contactFilter, hitBuffer, shellRadius);
        var d = new Dictionary<RaycastHit2D,float>();
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
                        //StartCoroutine(splat(hit.collider.gameObject));
                        GameObject.FindGameObjectWithTag("Player").GetComponent<PlayerPosUpdater>().setParams(20, .5f, 5f);
                        return true;
                    }
                }
            }
        }
        return false;
    }

    /*
    public IEnumerator splat(GameObject g)
    {

        Vector4[] arr = new Vector4[1];
        float[] arrray = new float[1];
        float max = Mathf.Sqrt(Mathf.Pow(g.GetComponent<SpriteRenderer>().sprite.bounds.extents.y, 2) + Mathf.Pow(g.GetComponent<SpriteRenderer>().sprite.bounds.extents.x, 2));
        arr[0] = g.transform.position;
        arrray[0] = 0.1f;
        while (arrray[0] <= max)
        {
            arrray[0] += Time.deltaTime * 10;
            Shader.SetGlobalVectorArray("_DaPoints", arr);
            Shader.SetGlobalFloatArray("_DaRays", arrray);
            Shader.SetGlobalInt("_DaPointsCount", 1);
            yield return new WaitForSeconds(.1f);
        }

    }
    */

    private void OnDrawGizmos()
    {
        Gizmos.color = Color.red;

    }

    private void Update()
    {
        Buffer();
    }

}
