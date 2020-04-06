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
        FindObjectOfType<GameManager>().GetComponent<GameManager>().setPause(false);
        SceneManager.LoadScene(SceneManager.GetActiveScene().buildIndex + 1);
    }

    public void ResetLevel()
    {
        Debug.Log("current " + currentLevel + " - RETRY");
        //SceneManager.LoadSceneAsync(SceneManager.GetActiveScene().name);
        FindObjectOfType<GameManager>().GetComponent<GameManager>().setPause(false); 
        SceneManager.LoadScene(SceneManager.GetActiveScene().buildIndex);
        
    }

    public void MainMenu()
    {
        Debug.Log("current " + currentLevel + " - MENU");
        FindObjectOfType<GameManager>().GetComponent<GameManager>().setPause(false); 
        SceneManager.LoadScene(0);
    }

    public void SetCurrentLevel(int currentLevel)
    {
        this.currentLevel = currentLevel;
    }
}
