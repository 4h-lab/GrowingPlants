using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class TestTransitionToLevel : MonoBehaviour
{
    // Start is called before the first frame update
    void Start()
    {
        SceneManager.LoadSceneAsync("MainMenu");
    }

    // Update is called once per frame
    void Update()
    {
        
    }
}
