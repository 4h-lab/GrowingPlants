using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class MovementJoystick : MonoBehaviour
{
    private EventEmitter ee;

    public float speed;
    public FixedJoystick variableJoystick;

    void Start() {
        ee = GameObject.FindGameObjectWithTag("EventEmitter").GetComponent<EventEmitter>();
    }

    public void FixedUpdate(){

            Vector3 direction = Vector3.right * variableJoystick.Horizontal;
            transform.position += direction * speed * Time.deltaTime  *GameManager.customTimeScale;
            /////////DEBUG
            if (Input.GetKey(KeyCode.A))
                transform.position -= Vector3.right * speed * Time.deltaTime * GameManager.customTimeScale;
            if (Input.GetKey(KeyCode.D))
               transform.position += Vector3.right * speed * Time.deltaTime * GameManager.customTimeScale;
        
    }
}
