using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class WinLevelContactInteractable : BaseContactInteractables
{
    private EventEmitter ee;

    private int currentLevel = 1;
    private int stars = 3;
    private float time = 0.0f;  //TODO: change to time format

    void Start()
    {
        ee = GameObject.FindGameObjectWithTag("EventEmitter").GetComponent<EventEmitter>();
        ee.on("win", notifyWin);
    }

    private void notifyWin(Object[] p)
    {
        Debug.Log("WIN. Level "+currentLevel+" - stars "+stars+" - time "+time);
    }

    public override void interact(GameObject initiator){
        Debug.Log("You won!!!!!!");
        ee.invoke("win", (new[] { this.gameObject }));
    }
}
