using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ParallaxBackground : MonoBehaviour{
    //[Tooltip("")]
    public float parallaxModifier = 1;

    private Vector3 previousPos;

    void Start(){
        previousPos = Camera.main.transform.position;
    }

    // I am using lateupdate because in this way i am sure that thecamera already moved for that frame
    void LateUpdate(){
        Vector3 delta = Camera.main.transform.position - previousPos;

        transform.position += new Vector3(delta.x * parallaxModifier, delta.y * parallaxModifier, transform.position.z);

        previousPos = Camera.main.transform.position;
        
    }
}
