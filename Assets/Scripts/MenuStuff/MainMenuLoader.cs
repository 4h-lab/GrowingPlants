using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class MainMenuLoader : MonoBehaviour{

    public void loadLevel(string levelname) {
        //todo: use lookup table to get buildindex
        SceneManager.LoadScene("Level_1");
        LoadSceneManager.loadNewLevel("Level_1");
    }

    public void loadSelectLevel()
    {
        SceneManager.LoadScene("LevelMenu");
    }

}
