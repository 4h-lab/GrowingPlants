 using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using UnityEngine.Events;

public class GameManager : MonoBehaviour {
    public static float customTimeScale = 1f;

    public static float totalTime = -1f;
    private static float timeSinceLevelStarted = 0f;

    EventEmitter ee;

    public delegate int CalcScoreStep(List<string> s);
    private List<string> notifiedAchievements;
    private BaseCalcScoreStep[] allSteps;
    public List<CalcScoreStep> scoreCalculator;

    private void Awake(){
        notifiedAchievements = new List<string>();
        scoreCalculator = new List<CalcScoreStep>();
    }

    private void Start(){
        ee = GameObject.FindGameObjectWithTag("EventEmitter").GetComponent<EventEmitter>();
        ee.on("win", (Object[] x) => { customTimeScale = 0f; totalTime = Time.realtimeSinceStartup - timeSinceLevelStarted; });
        timeSinceLevelStarted = Time.realtimeSinceStartup;
        customTimeScale = 1f;

        allSteps = GetComponents<BaseCalcScoreStep>();
        foreach (BaseCalcScoreStep bcss in allSteps) {
            scoreCalculator.Add(bcss.step);
            Debug.Log("Added new bcss");
        }
    }

    public float setPause(bool pause){
        float previousScale = customTimeScale;
        customTimeScale = pause ? 0f : 1f;
        Time.timeScale = pause ? 0f : 1f;
        return previousScale;
    }
    public float setWinPause(bool pause)
    {
        float previousScale = customTimeScale;
        customTimeScale = pause ? 0f : 1f;
        return previousScale;
    }
    public float setPause() {
        return setPause(customTimeScale != 0f);
    }

    public float GetCustomTimeScale(){
        return customTimeScale;
    }

    public void ControlsEnabled(bool enable){
        GameObject[] buttonList = GameObject.FindGameObjectsWithTag("Controls");
        foreach (GameObject b in buttonList)
        {
            b.GetComponent<Controls>().ControlsEnabled(enable);
        }
    }

    public void notifyOfNewSomething(string something, bool unique = false) {
        if (unique && notifiedAchievements.Contains(something)) return;
        notifiedAchievements.Add(something);
    }

    public int calcScore() {
        int totalscore = 0;
        foreach (CalcScoreStep css in scoreCalculator) { totalscore += css(notifiedAchievements);  }

        foreach (string s in notifiedAchievements) { Debug.Log("ACHIEVEMENT: " + s + " csss: " + scoreCalculator.Count + " notifachi: " + notifiedAchievements.Count); }
        return totalscore;
    }
}
