using System.Collections;
using System.Collections.Generic;
using UnityEngine;


public class PickUpInteractable : BaseContactInteractables{
    public override void interact(GameObject initiator){
        if (!(initiator.tag == "Player")) return;

        Debug.Log("pickup: " + initiator.name);

        GameObject.FindGameObjectWithTag("GameManager").GetComponent<GameManager>().notifyOfNewSomething("star.pickup");
        StartCoroutine(fadeAway(1.1f));
        Animator anim = GetComponentInChildren<Animator>();
        anim.SetTrigger("pickedUp");



    }

    private IEnumerator fadeAway(float t) {
        yield return new WaitForSeconds(t);
        Destroy(this.gameObject);
    }

}
