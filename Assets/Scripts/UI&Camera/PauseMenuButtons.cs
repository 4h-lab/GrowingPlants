using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class PauseMenuButtons : MonoBehaviour
{
    private int currentLevel = 0;

    private List<CanvasRenderer> objs= new List<CanvasRenderer>();


    private void Start()
    {
        for (int i = 0;i< this.transform.parent.childCount; i++){
            if (this.transform.parent.GetChild(i).GetComponent<CanvasRenderer>() != null)
            {
                objs.Add(this.transform.parent.GetChild(i).GetComponent<CanvasRenderer>());
            }
        }
    }
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


    private void LateUpdate()
    {
        if (Input.touchCount > 0)
        {
            foreach(CanvasRenderer r in objs)
            {
                r.SetAlpha(0);            }
        }
        else
        {
            foreach (CanvasRenderer r in objs)
            {
                r.SetAlpha(1);
            }
        }
    }
}
