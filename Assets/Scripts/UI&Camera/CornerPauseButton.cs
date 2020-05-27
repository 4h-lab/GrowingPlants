using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class CornerPauseButton : MonoBehaviour
{
    EventEmitter ee;

    [SerializeField] private GameObject pauseScreen;
    private GameObject instancedPauseScreen;

    void Start()
    {
        ee = GameObject.FindGameObjectWithTag("EventEmitter").GetComponent<EventEmitter>();
        ee.on("win", DisableButton);
    }

    public void Pause()
    {
        float previousScale = FindObjectOfType<GameManager>().GetComponent<GameManager>().setPause();
        if (previousScale != 0)
        {
            FindObjectOfType<GameManager>().GetComponent<GameManager>().ControlsEnabled(false);
            Canvas cv = GameObject.FindObjectOfType<Canvas>();
            instancedPauseScreen = GameObject.Instantiate(
                pauseScreen,
                cv.transform.position,
                Quaternion.identity,
                cv.transform);
        }
        else
        {
            Destroy(instancedPauseScreen);
            FindObjectOfType<GameManager>().GetComponent<GameManager>().ControlsEnabled(true);
        }
    }

    private void DisableButton(Object[] p)
    {
        gameObject.GetComponent<Button>().enabled = false;
    }
}