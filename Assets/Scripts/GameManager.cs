using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class GameManager : MonoBehaviour {
    public static float customTimeScale { get;  private set; } = 1f;

    EventEmitter ee;

    private void Start(){
        ee = GameObject.FindGameObjectWithTag("EventEmitter").GetComponent<EventEmitter>();
        ee.on("win", (Object[] x) => { customTimeScale = 0f; });
        customTimeScale = 1f;
    }

    public void setPause(bool pause){
        customTimeScale = pause ? 0f : 1f;
    }
    public void setPause() {
        customTimeScale = customTimeScale != 0f ? 0f :1f;
    }


}
