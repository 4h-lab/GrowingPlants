using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class GameManager : MonoBehaviour {
    public static float customTimeScale { get;  private set; } = 1f;

    public static float totalTime = -1f;
    private static float timeSinceLevelStarted = 0f;


    EventEmitter ee;

    private void Start(){
        ee = GameObject.FindGameObjectWithTag("EventEmitter").GetComponent<EventEmitter>();
        ee.on("win", (Object[] x) => { customTimeScale = 0f; totalTime = Time.realtimeSinceStartup - timeSinceLevelStarted; });
        timeSinceLevelStarted = Time.realtimeSinceStartup;
        customTimeScale = 1f;
    }

    public void setPause(bool pause){
        customTimeScale = pause ? 0f : 1f;
    }
    public void setPause() {
        customTimeScale = customTimeScale != 0f ? 0f :1f;
    }


}
