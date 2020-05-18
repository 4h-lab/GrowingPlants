using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class TEST_POPUP : MonoBehaviour{
    private string[] strings;
    private int index = 0;


    // Start is called before the first frame update
    void Start(){
        strings = new string[4]{"press A-D to move", "press to space to plant a seed", "beware of the water!", "plant a seed here to win"};
    }

    // Update is called once per frame
    void Update(){
        if (Input.GetMouseButtonDown(0)) {
            //Vector3 pos = Camera.main.ScreenToWorldPoint(Input.mousePosition);
            Vector3 pos = GameObject.FindGameObjectWithTag("Player").transform.position;

            PopupText.createNewPopup(new Vector3(pos.x, pos.y, -1), strings[index], Color.green);
            index++;
        }
    }
}
