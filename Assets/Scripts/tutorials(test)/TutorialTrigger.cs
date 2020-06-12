using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class TutorialTrigger : MonoBehaviour{
    public string eventTriggered;
    public Object[] eventparamenters;
    public bool selfDestructAfterTrigger = true;

    private void OnTriggerEnter2D(Collider2D collision) {
        GameObject.FindGameObjectWithTag("EventEmitter").GetComponent<EventEmitter>().invoke(eventTriggered, eventparamenters);
        if (selfDestructAfterTrigger) Destroy(this.gameObject);
    }
}
