using System.Collections;
using System.Collections.Generic;
using UnityEngine;


public class PickUpInteractable : BaseContactInteractables{
    public override void interact(GameObject initiator){
        if (!(initiator.tag == "Player")) return;
        
        GameObject.FindGameObjectWithTag("GameManager").GetComponent<GameManager>().notifyOfNewSomething("star.pickup");
        Destroy(this.gameObject);
    }

}
