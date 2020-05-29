
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class CornerPauseButton : MonoBehaviour
{
    EventEmitter ee;

    [SerializeField] private GameObject pauseScreen;
    private GameObject instancedPauseScreen;

    private GameManager gm;

    private CameraMovement cameraM;

    void Start()
    {
        ee = GameObject.FindGameObjectWithTag("EventEmitter").GetComponent<EventEmitter>();
        ee.on("win", DisableButton);
        gm = FindObjectOfType<GameManager>().GetComponent<GameManager>();
        cameraM = Camera.main.GetComponent<CameraMovement>();
    }

    public void Pause()
    {
        bool paused = gm.isPaused;
        if (!paused)
        {
            cameraM.follow = false;
            gm.setPause(true);
            gm.ControlsEnabled(false);
            Canvas cv = GameObject.FindObjectOfType<Canvas>();
            instancedPauseScreen = GameObject.Instantiate(
                pauseScreen,
                cv.transform.position,
                Quaternion.identity,
                cv.transform);
        }
        else
        {
            StartCoroutine(Unpause());
        }
    }

    private void DisableButton(Object[] p)
    {
        gameObject.GetComponent<Button>().enabled = false;
    }


    IEnumerator Unpause()
    {
        cameraM.follow = true;
        Destroy(instancedPauseScreen);
        yield return new WaitForEndOfFrame(); // let the camera start moving
        //yield return new WaitUntil(() => Camera.main.velocity == Vector3.zero);
        //while (!(Camera.main.velocity == Vector3.zero)) yield return null;
        while ((Mathf.Abs(Camera.main.velocity.y) > 0.1f)) yield return null;
        gm.setPause(false);
        gm.ControlsEnabled(true);
        yield return null;
    }


    /*public static float DistancePointToRectangle(Vector2 point, Rect rect)
    {
        //  Calculate a distance between a point and a rectangle.
        //  The area around/in the rectangle is defined in terms of
        //  several regions:
        //
        //  O--x
        //  |
        //  y
        //
        //
        //        I   |    II    |  III
        //      ======+==========+======   --yMin
        //       VIII |  IX (in) |  IV
        //      ======+==========+======   --yMax
        //       VII  |    VI    |   V
        //
        //
        //  Note that the +y direction is down because of Unity's GUI coordinates.

        if (point.x < rect.xMin)
        { // Region I, VIII, or VII
            if (point.y < rect.yMin)
            { // I
                Vector2 diff = point - new Vector2(rect.xMin, rect.yMin);
                return diff.magnitude;
            }
            else if (point.y > rect.yMax)
            { // VII
                Vector2 diff = point - new Vector2(rect.xMin, rect.yMax);
                return diff.magnitude;
            }
            else
            { // VIII
                return rect.xMin - point.x;
            }
        }
        else if (point.x > rect.xMax)
        { // Region III, IV, or V
            if (point.y < rect.yMin)
            { // III
                Vector2 diff = point - new Vector2(rect.xMax, rect.yMin);
                return diff.magnitude;
            }
            else if (point.y > rect.yMax)
            { // V
                Vector2 diff = point - new Vector2(rect.xMax, rect.yMax);
                return diff.magnitude;
            }
            else
            { // IV
                return point.x - rect.xMax;
            }
        }
        else
        { // Region II, IX, or VI
            if (point.y < rect.yMin)
            { // II
                return rect.yMin - point.y;
            }
            else if (point.y > rect.yMax)
            { // VI
                return point.y - rect.yMax;
            }
            else
            { // IX
                return 0f;
            }
        }
    }*/
}