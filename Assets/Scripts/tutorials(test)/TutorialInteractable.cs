using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class TutorialInteractable : MonoBehaviour{
    //public float size = 1;
    public string text = "";
    public Color color = Color.green;
    public float timeBeforeFading = 5f;
    private bool activated = false;

    private void OnTriggerEnter2D(Collider2D collision) {
        if (activated) return;
        Vector3 pos = new Vector3(transform.position.x, transform.position.y, -5);
        PopupText.createNewPopup(pos, text, color, timeBeforeFading);
        Destroy(this.gameObject);
        activated = true;
    }
}
