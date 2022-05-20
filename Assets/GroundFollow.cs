using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class GroundFollow : MonoBehaviour
{
    public GameObject Player;
    
    public float MaxSpeed = 5f;
    public float Acceleration = 100f;
    public float MinDistance = 1f;
    public float MaxDistance = 1f;

      public float MaxVerticalDistance = 1f;

    private Rigidbody2D _rigidbody;


    // Start is called before the first frame update
    void Start()
    {
        _rigidbody = GetComponent<Rigidbody2D>();
    }

    // Update is called once per frame
    void Update()
    {
        if (Player == null) 
        {
            Player = GameObject.FindWithTag("Player");
        }

        // Move towards the player
        var target = Player.transform;
        var distance = Vector3.Distance(transform.position, target.position);

        if (MinDistance < distance && distance < MaxDistance)
        {
            var dir = (target.position - transform.position).normalized;
            //Check if dir is facing left or right
            MoveHorizontally(dir);
            if (distance < MaxVerticalDistance)
            {
                MoveVertically(dir);
            }
        }
    }

    //The time since last jump
    private float _timeSinceJump = 0f;
    
    void MoveVertically(Vector3 dir)
    {
        

        _timeSinceJump += Time.deltaTime;
        if (dir.y > 0)
        {
            if (_timeSinceJump > 2.0f)
            {
                _rigidbody.AddForce(Vector2.up * Acceleration * 500f);
                _timeSinceJump = 0;
            }
        }
    
    }

    void MoveHorizontally(Vector3 dir)
    {
        if (dir.x < 0)
        {
            dir = new Vector3(-1, 0, 0);
        } else {
            dir = new Vector3(1, 0, 0);
        }

        // Add force to this rigidbody to move it towards the target if the max speed isn't exceeded
        if (_rigidbody.velocity.magnitude < MaxSpeed || Vector3.Angle(dir, _rigidbody.velocity) > 90)
        {
            _rigidbody.AddForce(dir * Acceleration * Time.deltaTime * 1000);
        }
    }
}
