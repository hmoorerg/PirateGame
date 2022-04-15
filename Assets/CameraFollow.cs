using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using System;

public class CameraFollow : MonoBehaviour
{
    public float FollowSpeed;
    public float yMax =  10;
    public float yMin = 0;
    public Transform target;
    // Start is called before the first frame update
    void Start()
    {
        FollowSpeed = 2f;
    }

    // Update is called once per frame
    void Update()
    {
        float yTarget = Math.Min(target.position.y, yMax);
        yTarget = Math.Max(yTarget, yMin);
        Vector3 newPos = new Vector3(target.position.x, yTarget, -10f);
        transform.position = Vector3.Slerp(transform.position, newPos, FollowSpeed*Time.deltaTime);
        
    }
}
