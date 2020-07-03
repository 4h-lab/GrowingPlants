using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using TMPro;
using UnityEngine.UI;
using UnityEngine.SceneManagement;
using System;
using UnityEngine.Events;

public class LoadLevels : MonoBehaviour
{
    [SerializeField]
    private GameObject button;
    private Dictionary<int, SavedGameData.LevelData> levels;
    [SerializeField]
    private GameObject NormalPanel;
    [SerializeField]
    private GameObject NightmarePanel;

    private SavedGameData.LevelData.levelType filter = SavedGameData.LevelData.levelType.nornal; 

    // Start is called before the first frame update
    void Start()
    {
        requestNewLevels(NormalPanel, SavedGameData.LevelData.levelType.nornal);
        requestNewLevels(NightmarePanel, SavedGameData.LevelData.levelType.nightmare);
        NightmarePanel.SetActive(false);
    }

    private void loadLevel(int l)
    {
        LoadSceneManager.loadNewLevel(l);
    }
    // Update is called once per frame

        public void back()
    {
        SceneManager.LoadScene(0);
    }
    void Update()
    {
        
    }

    private void requestNewLevels(GameObject p, SavedGameData.LevelData.levelType t) {
        levels = SavedGameData.gamedata.getLevelInfos();
        UnityAction startView;
        foreach (KeyValuePair<int, SavedGameData.LevelData> level in levels) {
            if (level.Value.lt == t) {
                var c = Instantiate(button, p.transform);
                c.transform.GetComponentInChildren<TextMeshProUGUI>().text = (level.Value.levelID - 1).ToString();
                startView = () => loadLevel(level.Value.levelID);
                if (level.Value.unlocked) {
                    c.GetComponent<Button>().onClick.AddListener(startView);
                } else {
                    c.GetComponentInChildren<Button>().interactable = false;
                }
                var stars = level.Value.stars > 3 ? 3 : level.Value.stars;
                for (int i = 0; i < stars; i++) {
                    c.transform.Find("Stars").transform.GetChild(i).GetComponent<Image>().color = new Color(1f, 1f, 1f, 1f);
                }
            }
        }
    }


    public void getNightmare()    {
        if (NormalPanel.activeSelf) {
            NormalPanel.SetActive(false);
            NightmarePanel.SetActive(true);
        } else {
            NormalPanel.SetActive(true);
            NightmarePanel.SetActive(false);
        }
        
    }

   
    

}
