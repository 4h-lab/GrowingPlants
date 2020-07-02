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

    
    // Start is called before the first frame update
    void Start()
    {
        levels = SavedGameData.gamedata.getLevelInfos();
        UnityAction startView;
        foreach (KeyValuePair<int, SavedGameData.LevelData> level in levels)
        {
            var c = Instantiate(button, this.transform);
            c.transform.GetComponentInChildren<TextMeshProUGUI>().text = (level.Value.levelID-1).ToString();
            startView = () => loadLevel(level.Value.levelID);
            if (level.Value.unlocked) { 
            c.GetComponent<Button>().onClick.AddListener(startView);
            }
            else
            {
                c.GetComponentInChildren<Button>().interactable = false;
            }
            var stars = level.Value.stars > 3 ? 3 : level.Value.stars;
            for (int i=0;i<stars; i++)
            {
                c.transform.Find("Stars").transform.GetChild(i).GetComponent<Image>().color = new Color(1f, 1f, 1f, 1f);
            }

        }
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
}
