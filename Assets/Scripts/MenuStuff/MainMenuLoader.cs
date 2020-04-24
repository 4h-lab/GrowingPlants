using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class MainMenuLoader : MonoBehaviour{

    public void loadLevel(string levelname) {
        //todo: use lookup table to get buildindex
        SceneManager.LoadSceneAsync("Lv1");       
    }

    public void loadSelectLevel()
    {
        SceneManager.LoadScene("LevelMenu");
    }

}
