using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class BasePlant : MonoBehaviour
{
    public float maxHeigth;
    public float growthSpeed;
    protected float initY;
    protected GameObject spawner;

    protected EventEmitter ee;
    protected GameObject player;
    protected bool stopped = false;
    protected float ray_point;
    protected float small_radius = 0.01f;

    public GameObject GetSpawner() => this.spawner;

    public void SetSpawner(GameObject spawner)
    {
        this.spawner = spawner;
    }

    public float GetInitY()
    {
        return initY;
    }

    public float GetMaxHeigth()
    {
        return maxHeigth;
    }
}
