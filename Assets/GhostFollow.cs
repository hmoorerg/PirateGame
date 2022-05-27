using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class GhostFollow : MonoBehaviour
{
    private GameObject _player;
    
    public float MaxSpeed = 2f;
    public float friction = 0.6f;
    public float Acceleration = 100f;

    public float MaxFollowDistance = 4f;

    private Rigidbody2D _rigidbody;




    // Start is called before the first frame update
    void Start()
    {
        _rigidbody = GetComponent<Rigidbody2D>();
    }

    // Update is called once per frame
    void Update()
    {
        if (_player == null) 
        {
            _player = GameObject.FindGameObjectWithTag("Player");
            return;
        }

        if (Vector3.Distance(transform.position, _player.transform.position) <= MaxFollowDistance)
        {
            transform.LookAt(_player.transform);
            
            // Move towards the player
            var target = _player.transform;
            var dir = target.position - transform.position;
            // Add force to this rigidbody to move it towards the target if the max speed isn't exceeded
            if (_rigidbody.velocity.magnitude< MaxSpeed)
            {
                _rigidbody.AddForce(dir.normalized * Acceleration * Time.deltaTime * 1000);
            }
        }
        
        Vector2 vel = _rigidbody.velocity;
        //clamps speed at max speed
        if(vel.x > MaxSpeed){ vel.x = MaxSpeed; }
        if(vel.x < -MaxSpeed){ vel.x = -MaxSpeed; }

        //applies a simulated friction
        vel.x = vel.x*friction;
        _rigidbody.velocity = vel;
    }
}
