using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using TMPro;
using System;

public class PopupText: MonoBehaviour{
    public static Func<Vector3, float, Vector3> utilFuncs_moveUp = (Vector3 _pos, float deltatime) => _pos + Vector3.up * (deltatime * .3f);
    public static Func<Vector3, float, Vector3> utilFuncs_moveDown = (Vector3 _pos, float deltatime) => -utilFuncs_moveUp(_pos, deltatime);
    public static Func<Vector3, float, Vector3> utilFuncs_moveRight = (Vector3 _pos, float deltatime) => _pos + Vector3.right * (deltatime * .3f);
    public static Func<Vector3, float, Vector3> utilFuncs_moveLeft = (Vector3 _pos, float deltatime) => -utilFuncs_moveRight(_pos, deltatime);
    public static Func<Vector3, float, Vector3> utilFuncs_dontMove = (Vector3 _pos, float deltatime) => _pos;
  
    private TextMeshPro popupTextMesh;
    private float timeBeforeFading;
    private float timeToFade;
    private float startTime;
    private Func<Vector3, float, Vector3> moveFunc;


    private void Awake() {
        popupTextMesh = GetComponent<TextMeshPro>();
    }

    /// <summary>
    /// This instantiate and initialize a new instance of popuptext
    /// </summary>
    /// <param name="pos">the world position where the popup should appear</param>
    /// <param name="text">the text that should be displayed in the popup</param>
    /// <param name="color">the color of the text</param>
    /// <param name="_timeBeforeFading">the time (in seconds ) before the text start disappearing (default = 5sec)</param>
    /// <param name="_timeToFade"> the time (in seconds ) in which the text completely disappear (default = 2sec)</param>
    /// <param name="_moveFunc"> the function to move the text while it's still alive; moveFunc takes a float deltatime and a vector3 actualpos as parameters, and returns the vector3 newpos</param>
    public static void createNewPopup(Vector3 pos, string text, Color color, float _timeBeforeFading = 5f, float _timeToFade = 2f, Func<Vector3, float, Vector3> _moveFunc = null ) {
        GameObject obj = Instantiate(GameObject.FindGameObjectWithTag("GameManager").GetComponent<ResourceLoader>().popupPrefab , pos, Quaternion.identity);

        PopupText popup = obj.GetComponent<PopupText>();
        popup.startTime = Time.time;
        popup.timeBeforeFading = _timeBeforeFading;
        popup.timeToFade = _timeToFade;


        popup.moveFunc = _moveFunc != null ? _moveFunc : utilFuncs_moveUp;
        popup.show(text, color);
    }


    private void show(string text, Color color) {
        popupTextMesh.text = text;
        popupTextMesh.color = color;
        StartCoroutine(lifetime());
    }

    public IEnumerator lifetime() {
        float age = 0;
        while (true) {
            float deltatime = (Time.time - startTime); // we need to do this instead of time.deltatime because we are not updating this thing every frame
            age += deltatime;
            transform.position = moveFunc(transform.position, deltatime);
            if (age < timeBeforeFading) {
                // first part of our lifetime.....

            } else {
                if (age < timeBeforeFading + timeToFade) {
                    // start fading................................
                    Color c = popupTextMesh.color;
                    c.a -= deltatime / timeToFade;
                    popupTextMesh.color = c;
                } else {
                    Destroy(this.gameObject);
                }
            }
            
            yield return new WaitForSeconds(.05f);
        }
    }

}
