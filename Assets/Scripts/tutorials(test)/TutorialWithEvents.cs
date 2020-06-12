using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class TutorialWithEvents : MonoBehaviour
{
    private EventEmitter ee;
    private float timer = 0;

    private bool movementActivated = false;
    private bool plantActivated = false;

    private Animator arrowAnim;
    private Animator plantAnim;


    private void Start() {
        ee = GameObject.FindGameObjectWithTag("EventEmitter").GetComponent<EventEmitter>();
        ee.on("playermoved", handleMovementControls );
        ee.on("plantplanted", handlePlantControls);
        ee.on("winsoilreached", handleWinSoilPlantButton);

        arrowAnim = GameObject.FindGameObjectWithTag("UI_Arrows").GetComponent<Animator>();
        plantAnim = GameObject.FindGameObjectWithTag("UI_PlantButton").GetComponent<Animator>();


    }

    private void Update() {
        timer += Time.deltaTime;

        if (timer > 3 && !movementActivated) {
            arrowAnim.SetBool("blip", true);
            ee.on("playermoved", stopMovementTutorial);
        }
        if(timer > 6 && !plantActivated) {
            plantAnim.SetBool("blip", true);
            ee.on("plantplanted", stopPlantTutorial);
        }

    }

    private void handleMovementControls(object[] x) {
        movementActivated = true;
    }
    private void stopMovementTutorial(object[] x) {
        arrowAnim.SetBool("blip", false);
    }

    private void handlePlantControls(object[] x) {
        plantActivated = true;
    }
    private void stopPlantTutorial(object[] x) {
        plantAnim.SetBool("blip", false);
    }
    private void handleWinSoilPlantButton(object[] x) {
        plantAnim.SetBool("blip", true);
    }
}
