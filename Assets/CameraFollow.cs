using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using System;

public class CameraFollow : MonoBehaviour
{
    public float FollowSpeed;
    public Transform target;

    public float yOffset = 2;
    // Start is called before the first frame update
    void Start()
    {
        FollowSpeed = 2f;
    }

    // Update is called once per frame
    void Update()
    {
        Vector3 newPos = new Vector3(target.position.x, target.position.y+yOffset, -10f);
        transform.position = Vector3.Slerp(transform.position, newPos, FollowSpeed*Time.deltaTime);
        
    }
}
