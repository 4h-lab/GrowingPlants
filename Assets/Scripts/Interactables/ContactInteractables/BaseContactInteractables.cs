using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public abstract class BaseContactInteractables : BaseInteractable{


    private void OnTriggerEnter(Collider other){
        GameObject go = other.gameObject;
        interact(go);
    }


}
