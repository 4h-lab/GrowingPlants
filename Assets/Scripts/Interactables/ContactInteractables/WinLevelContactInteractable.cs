using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using TMPro;
using UnityEngine.SceneManagement;


public class WinLevelContactInteractable : BaseContactInteractables
{
    private EventEmitter ee;
    [SerializeField] GameObject winScreen;

    private int currentLevel = 1;
    private int stars = 3;
    public float time  { get; private set; } = 0.0f;  //TODO: change to time format

    void Start()
    {
        ee = GameObject.FindGameObjectWithTag("EventEmitter").GetComponent<EventEmitter>();
        ee.on("win", notifyWin);
        time = Time.realtimeSinceStartup;
    }

    private void notifyWin(Object[] p)
    {
        //placeholder
        int currentLevel = 1;

        FindObjectOfType<GameManager>().GetComponent<GameManager>().ControlsEnabled(false);
        Debug.Log("WIN. Level "+currentLevel+" - stars "+stars+" - time "+time);
        GameObject popup = GameObject.Instantiate(
            winScreen, 
            Vector3.zero, 
            Quaternion.identity, 
            GameObject.FindObjectOfType<Canvas>().transform);

        GameManager gm = GameObject.FindGameObjectWithTag("GameManager").GetComponent<GameManager>();
        gm.notifyOfNewSomething("level.finished");
        stars = gm.calcScore();
        string obtainedStars = "";
        for (int i = 0; i < stars; i++) obtainedStars += " *";



        popup.transform.Find("StarsTextR").gameObject.GetComponent<TextMeshProUGUI>().text = obtainedStars;

        time = Time.realtimeSinceStartup - time;
        popup.transform.Find("TimeTextR").gameObject.GetComponent<TextMeshProUGUI>().text = formatGameTime(time);
        popup.transform.Find("PauseMenuButtons").gameObject.GetComponent<PauseMenuButtons>().SetCurrentLevel(currentLevel);
        
    }

    public override void interact(GameObject initiator){
        Debug.Log("You won!!!!!!");
        ee.invoke("win", (new[] { this.gameObject }));
    }

    private static string formatGameTime(float seconds) {
        Debug.Log(seconds);
        int minutes = Mathf.FloorToInt(seconds / 60);
        int s = Mathf.FloorToInt(seconds % 60);
        int decimals = Mathf.FloorToInt( (seconds*100)%100);

        string res =  (minutes < 10 ? "0" + minutes.ToString() : minutes.ToString());
        res += ": " + (s < 10 ? "0" + s.ToString() : s.ToString());
        res += ": " + (decimals < 10 ? "0" + decimals.ToString() : decimals.ToString());
        return res;
    }


}
