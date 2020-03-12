using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Movement : MonoBehaviour
{
    // Start is called before the first frame update
    [SerializeField]
    private float speed = 1.0f;
    // Start is called before the first frame update
    void Start()
    {

    }

    // Update is called once per frame
    void Update()
    {
        if (Input.GetKey(KeyCode.A))
            transform.position -= Vector3.right * speed * Time.deltaTime;
        if (Input.GetKey(KeyCode.D))
            transform.position += Vector3.right * speed * Time.deltaTime;
    }

    public void move(Vector2 dir)
    {
        this.transform.position = dir * speed * Time.deltaTime;
    }
    public void moveRight()
    {
        this.transform.position += Vector3.right * speed * Time.deltaTime;
    }
    public void moveLeft()
    {
        this.transform.position += -Vector3.right * speed * Time.deltaTime;
    }
}
