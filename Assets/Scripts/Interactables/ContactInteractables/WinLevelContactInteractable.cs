using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class WinLevelContactInteractable : BaseContactInteractables{


    public override void interact(GameObject initiator){
        Debug.Log("You won!!!!!!");
        SceneManager.LoadSceneAsync("prereleaseWinScene");
    }
}
