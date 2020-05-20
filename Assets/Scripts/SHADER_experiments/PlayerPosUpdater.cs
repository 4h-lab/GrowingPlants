using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerPosUpdater : MonoBehaviour{
    [Tooltip("If set to false, the player will stop splatting colors")]
    public bool stopColor = false;
    [Tooltip("Number of generated points around the player; it should be 0 <= number <= 20 for shader reasons ")]
    public int pointsGenerated = 10;
    private int pg;
    [Tooltip("The radius for each color splat; note that this number will be somewhat randomized")]
    public float baseSplatRadius = 1f;
    private float bsr;
    [Tooltip("The radius around the player in which a color splat can be thrown")]
    public float baseThrownRadius = 2f;
    private float btr;

    [Tooltip("The Frequency at which the texture are updated in seconds")]
    public float frequency = 0.1f;

    private Vector3 center;
    private float offset;
    private void Awake()
    {
        //reset the arrays to delete past iteration --> you can't pass arrays of different size to a shader; if you initialize and array with a size of 1 then you cant pass an array of size 10
        Vector4[] points = new Vector4[20];
        float[] rays = new float[20];
        for (int i = 0; i < 20; i++) {
            points[i] = Vector4.positiveInfinity;
            rays[i] = 0f;
        }
        Shader.SetGlobalVectorArray("_DaPoints", points);
        Shader.SetGlobalFloatArray("_DaRays", rays);
        Shader.SetGlobalInt("_DaPointsCount", 1);
        
    }
    void Start(){
        center = transform.position -Vector3.up*GetComponent<BoxCollider2D>().bounds.extents.y * 2;
        offset = GetComponent<BoxCollider2D>().bounds.extents.y * 2;
        pg = pointsGenerated;
        bsr = baseSplatRadius;
        btr = baseThrownRadius;
        StartCoroutine(splat());
    }

    /// <summary>
    /// set the parameters for the NEXT splat. the values will be resetted after that
    /// </summary>
    /// <param name="points">the number of balls to be generated</param>
    /// <param name="splat">the general radius for each ball</param>
    /// <param name="dist">the area in which the balls can be generated</param>
    public void setParams(int points, float splat, float dist) {
        pg = points;
        bsr = splat;
        btr = dist;
    }


    public IEnumerator splat() {
        while (!stopColor) {
            x();
            yield return new WaitForSeconds(frequency);
        }
    }

    private void x() {
        Vector4[] arr = new Vector4[pg];
        float[] arrray = new float[pg];
        
        for (int i = 0; i < pg; i++) {
            
            //arr[i] = new Vector4(center.x + Random.Range(-btr, btr), center.y + Random.Range(-btr, btr), 0, 0);
            arr[i] = center + Random.insideUnitSphere * btr;
            arrray[i] = Mathf.Min(bsr / Vector2.Distance((Vector2)arr[i], (Vector2)transform.position), bsr);
            
        }

        Shader.SetGlobalVectorArray("_DaPoints", arr);
        Shader.SetGlobalFloatArray("_DaRays", arrray);
        Shader.SetGlobalInt("_DaPointsCount", pg);


        // after updating the points, the values are resetted;
        pg = pointsGenerated;
        bsr = baseSplatRadius;
        btr = baseThrownRadius;
        center = transform.position - Vector3.up * offset;
    }

    private void OnDrawGizmos()
    {
        Gizmos.DrawWireSphere(center, baseThrownRadius);
    }

    private void OnDestroy()
    {
        
    }
}
