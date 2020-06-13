using System.Collections;
using System.Collections.Generic;
using UnityEngine;


public class PickUpInteractable : BaseContactInteractables{
    public override void interact(GameObject initiator){
        if (!(initiator.tag == "Player")) return;

        Debug.Log("pickup: " + initiator.name);

        GameObject.FindGameObjectWithTag("GameManager").GetComponent<GameManager>().notifyOfNewSomething("star.pickup");
        StartCoroutine(fadeAway(1.1f));
        
        // play pickedup animation
        Animator anim = GetComponentInChildren<Animator>();
        if (anim != null) anim.SetTrigger("pickedUp");

        // play pickedup sound
        AudioSource audios = GetComponentInChildren<AudioSource>();
        if (audios != null) AudioSource.PlayClipAtPoint(audios.clip, transform.position);


    }

    private IEnumerator fadeAway(float t) {
        yield return new WaitForSeconds(t);
        Destroy(this.gameObject);
    }

}
