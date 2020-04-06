using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class DEBUG_CONTROL : MonoBehaviour{
    
    
    void Start(){
        
        
    }
    
    
    void Update(){
        if(Input.GetKeyDown(KeyCode.Tab))SceneManager.LoadScene(SceneManager.GetActiveScene().buildIndex + 1);
        if(Input.GetKeyDown(KeyCode.LeftControl))SceneManager.LoadScene(Mathf.Max(0, SceneManager.GetActiveScene().buildIndex - 1));

    }
}
