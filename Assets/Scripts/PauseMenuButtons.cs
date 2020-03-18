using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class PauseMenuButtons : MonoBehaviour
{
    private int currentLevel = 0;

    public void NextLevel()
    {
        Debug.Log("current " + currentLevel + " - NEXT");
    }

    public void ResetLevel()
    {
        Debug.Log("current " + currentLevel + " - RETRY");
        SceneManager.LoadSceneAsync(SceneManager.GetActiveScene().name);
    }

    public void MainMenu()
    {
        Debug.Log("current " + currentLevel + " - MENU");
    }

    public void SetCurrentLevel(int currentLevel)
    {
        this.currentLevel = currentLevel;
    }
}
