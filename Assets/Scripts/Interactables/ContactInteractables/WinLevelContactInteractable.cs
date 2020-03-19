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

        Debug.Log("WIN. Level "+currentLevel+" - stars "+stars+" - time "+time);
        GameObject popup = GameObject.Instantiate(
            winScreen, 
            Vector3.zero, 
            Quaternion.identity, 
            GameObject.FindObjectOfType<Canvas>().transform);
        string obtainedStars = "";
        for (int i = 0; i < stars; i++) obtainedStars += " *";
        popup.transform.Find("StarsTextR").gameObject.GetComponent<TextMeshProUGUI>().text = obtainedStars;

        time = Time.realtimeSinceStartup - time;
        popup.transform.Find("TimeTextR").gameObject.GetComponent<TextMeshProUGUI>().text = time.ToString();
        popup.transform.Find("PauseMenuButtons").gameObject.GetComponent<PauseMenuButtons>().SetCurrentLevel(currentLevel);
        
    }

    public override void interact(GameObject initiator){
        Debug.Log("You won!!!!!!");
        ee.invoke("win", (new[] { this.gameObject }));
    }
}
