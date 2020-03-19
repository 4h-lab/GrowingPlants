using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class PauseMenuButtons : MonoBehaviour
{
    private int currentLevel = 0;
    [SerializeField] private GameObject pauseScreen;
    private GameObject instancedPauseScreen;

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

    public void Pause()
    {
        float previousScale = FindObjectOfType<GameManager>().GetComponent<GameManager>().setPause();
        if (previousScale != 0)
        {
            Debug.Log("PAUSE");
            instancedPauseScreen = GameObject.Instantiate(
                pauseScreen,
                Vector3.zero,
                Quaternion.identity,
                GameObject.FindObjectOfType<Canvas>().transform);
        }
        else
        {
            Destroy(instancedPauseScreen);
        }
    }

    public void SetCurrentLevel(int currentLevel)
    {
        this.currentLevel = currentLevel;
    }
}
