using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class GameManager : MonoBehaviour {
    public static float customTimeScale = 1f;

    public static float totalTime = -1f;
    private static float timeSinceLevelStarted = 0f;


    EventEmitter ee;

    private void Start(){
        ee = GameObject.FindGameObjectWithTag("EventEmitter").GetComponent<EventEmitter>();
        ee.on("win", (Object[] x) => { customTimeScale = 0f; totalTime = Time.realtimeSinceStartup - timeSinceLevelStarted; });
        timeSinceLevelStarted = Time.realtimeSinceStartup;
        customTimeScale = 1f;
    }

    public float setPause(bool pause){
        float previousScale = customTimeScale;
        customTimeScale = pause ? 0f : 1f;
        return previousScale;
    }

    public float setPause() {
        float previousScale = customTimeScale;
        customTimeScale = customTimeScale != 0f ? 0f :1f;
        return previousScale;
    }

    public float GetCustomTimeScale()
    {
        return customTimeScale;
    }

    public void ControlsEnabled(bool enable)
    {
        GameObject[] buttonList = GameObject.FindGameObjectsWithTag("Controls");
        foreach (GameObject b in buttonList)
        {
            b.GetComponent<Button>().enabled = enable;
        }
    }
}
