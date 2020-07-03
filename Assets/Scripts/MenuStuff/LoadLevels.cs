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
        requestnewlevelsNormal();
        requestnewlevelsNightmare();
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

    private void requestnewlevelsNormal() {
        levels = SavedGameData.gamedata.getLevelInfos();
        UnityAction startView;
        foreach (KeyValuePair<int, SavedGameData.LevelData> level in levels) {
            if (level.Value.lt == SavedGameData.LevelData.levelType.nornal) {
                var c = Instantiate(button, NormalPanel.transform);
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
    private void requestnewlevelsNightmare()
    {
        levels = SavedGameData.gamedata.getLevelInfos();
        UnityAction startView;
        foreach (KeyValuePair<int, SavedGameData.LevelData> level in levels)
        {
            if (level.Value.lt == SavedGameData.LevelData.levelType.nightmare)
            {
                var c = Instantiate(button, NightmarePanel.transform);
                c.transform.GetComponentInChildren<TextMeshProUGUI>().text = (level.Value.levelID - 1).ToString();
                startView = () => loadLevel(level.Value.levelID);
                if (level.Value.unlocked)
                {
                    c.GetComponent<Button>().onClick.AddListener(startView);
                }
                else
                {
                    c.GetComponentInChildren<Button>().interactable = false;
                }
                var stars = level.Value.stars > 3 ? 3 : level.Value.stars;
                for (int i = 0; i < stars; i++)
                {
                    c.transform.Find("Stars").transform.GetChild(i).GetComponent<Image>().color = new Color(1f, 1f, 1f, 1f);
                }
            }
        }
    }

    public void getNightmare()
    {
        NormalPanel.SetActive(false);
        NightmarePanel.SetActive(true);
    }
    

}
