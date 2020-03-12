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
    }
}
