using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class MovementJoystick : MonoBehaviour
{
    private EventEmitter ee;

    public float maxSpeed;
    public float acceleration;
    private float speed;

    public FixedJoystick variableJoystick;

    void Start() {
        ee = GameObject.FindGameObjectWithTag("EventEmitter").GetComponent<EventEmitter>();
        speed = 0f;
    }

    public void FixedUpdate(){
        if (variableJoystick.Horizontal != 0f) {
            Vector3 dir = Vector3.right * Mathf.Sign(variableJoystick.Horizontal);
            movePlayer(dir);
        } 
        else if (Input.GetKey(KeyCode.A)) movePlayer(Vector3.left);
        else if (Input.GetKey(KeyCode.D)) movePlayer(Vector3.right);
        else speed = 0;
    }
    private void movePlayer(Vector3 dir) {
        speed += acceleration * Time.deltaTime;
        if (speed > maxSpeed) speed = maxSpeed;

        transform.position += dir * speed * Time.deltaTime * GameManager.customTimeScale;
    
    }

}
