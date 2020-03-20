using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class PlantButton : MonoBehaviour{
    //GameObject player
    void Update()
    {
        if (Input.GetKeyDown(KeyCode.Space) && FindObjectOfType<GameManager>().GetComponent<GameManager>().GetCustomTimeScale() != 0f)
        {
            //TODO: change to simulate click if needed
            //https://answers.unity.com/questions/945299/how-to-trigger-a-button-click-from-script.html
            gameObject.GetComponent<Button>().onClick.Invoke();
            //gameObject.GetComponent<>
        }
    }
}
