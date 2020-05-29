using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ArrowManager : MonoBehaviour
{
    private GameObject up;
    private GameObject down;
    private GameObject left;
    private GameObject right;

    private Vector4 bounds;
   
    // Start is called before the first frame update
    void Start()
    {
        up = this.transform.GetChild(0).gameObject;
        down= this.transform.GetChild(1).gameObject;
        left= this.transform.GetChild(2).gameObject;
        right= this.transform.GetChild(3).gameObject;
        bounds = Camera.main.GetComponent<CameraMovement>().levelBounds();
        checkArrows();
    }

    // Update is called once per frame
    void Update()
    {
        checkArrows();
    }

    void checkArrows()
    {
        //maxY
        if (Camera.main.transform.position.y + (Camera.main.rect.size.y / 2) > bounds.w)
        {
            up.SetActive(false);
        }
        else { up.SetActive(true); }

        //minY
        if (Camera.main.transform.position.y - (Camera.main.rect.size.y / 2) < bounds.z)
        {
            down.SetActive(false);
        }
        else { down.SetActive(true); }

        //maxX
        if (Camera.main.transform.position.x + (Camera.main.rect.size.x / 2) > bounds.y)
        {
            right.SetActive(false);
        }
        else { right.SetActive(true); }

        //minX
        if (Camera.main.transform.position.x - (Camera.main.rect.size.x / 2) < bounds.x)
        {
            left.SetActive(false);
        }
        else { left.SetActive(true); }
    }
}
