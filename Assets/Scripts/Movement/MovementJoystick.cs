using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class MovementJoystick : MonoBehaviour
{
    private EventEmitter ee;

    public float speed;
    public FixedJoystick variableJoystick;
    private bool canMove = true;

    void Start()
    {
        ee = GameObject.FindGameObjectWithTag("EventEmitter").GetComponent<EventEmitter>();
        ee.on("win", StopPlayerMovement);
    }

    public void FixedUpdate()
    {

        if (canMove)
        {
            Vector3 direction = Vector3.right * variableJoystick.Horizontal;
            transform.position += direction * speed * Time.deltaTime;
            /////////DEBUG
            if (Input.GetKey(KeyCode.A))
                transform.position -= Vector3.right * speed * Time.deltaTime;
            if (Input.GetKey(KeyCode.D))
                transform.position += Vector3.right * speed * Time.deltaTime;
        }
    }

    public void StopPlayerMovement(Object[] p)
    {
        canMove = false;
    }
}
