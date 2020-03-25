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
        Debug.Log(variableJoystick.Horizontal);
        
        if (variableJoystick.Horizontal != 0f) {
            Vector3 dir = Vector3.right * Mathf.Sign(variableJoystick.Horizontal);
            movePlayer(dir);
        } 
        else if (Input.GetKey(KeyCode.A)) movePlayer(Vector3.left);
        else if (Input.GetKey(KeyCode.D)) movePlayer(Vector3.right);
        else speed = 0;

        /*
            Vector3 direction = Vector3.right * variableJoystick.Horizontal;
            transform.position += direction * speed * Time.deltaTime  *GameManager.customTimeScale;
            /////////DEBUG
            if (Input.GetKey(KeyCode.A))
                transform.position -= Vector3.right * speed * Time.deltaTime * GameManager.customTimeScale;
            if (Input.GetKey(KeyCode.D))
               transform.position += Vector3.right * speed * Time.deltaTime * GameManager.customTimeScale;
        */
    }
    private void movePlayer(Vector3 dir) {
        speed += acceleration * Time.deltaTime;
        if (speed > maxSpeed) speed = maxSpeed;

        transform.position += dir * speed * Time.deltaTime * GameManager.customTimeScale;
    
    }

}
