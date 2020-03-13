using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class MovementJoystick : MonoBehaviour
{
    public float speed;
    public FixedJoystick variableJoystick;

    public void FixedUpdate()
    {
        Vector3 direction =  Vector3.right * variableJoystick.Horizontal;
        transform.position += direction * speed * Time.deltaTime;
        /////////DEBUG
        if (Input.GetKey(KeyCode.A))
            transform.position -= Vector3.right * speed * Time.deltaTime;
        if (Input.GetKey(KeyCode.D))
            transform.position += Vector3.right * speed * Time.deltaTime;
    }
}
