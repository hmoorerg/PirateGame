using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class RTSCameraMove : MonoBehaviour
{
    // Start is called before the first frame update
    public float baseSpeed = 5;
    public float maxSpeed = 20;
    void Start()
    {

    }

    // Update is called once per frame
    void Update()
    {
        var speed = baseSpeed;
        if (Input.GetKey(KeyCode.LeftShift))
        {
            speed = maxSpeed;
        }


        if (Input.GetKey(KeyCode.D))
        {
            transform.Translate(new Vector3(speed * Time.deltaTime, 0, 0));
        }
        if (Input.GetKey(KeyCode.A))
        {
            transform.Translate(new Vector3(-speed * Time.deltaTime, 0, 0));
        }
        if (Input.GetKey(KeyCode.S))
        {
            transform.Translate(new Vector3(0, -speed * Time.deltaTime, 0));
        }
        if (Input.GetKey(KeyCode.W))
        {
            transform.Translate(new Vector3(0, speed * Time.deltaTime, 0));
        }

    }
}
