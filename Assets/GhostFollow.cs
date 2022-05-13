using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class GhostFollow : MonoBehaviour
{
    public GameObject Player;
    
    public float MaxSpeed = 5f;
    public float Acceleration = 100f;

    private Rigidbody2D _rigidbody;


    // Start is called before the first frame update
    void Start()
    {
        _rigidbody = GetComponent<Rigidbody2D>();
    }

    // Update is called once per frame
    void Update()
    {
        if (Player is null) 
        {
            Player = GameObject.FindGameObjectWithTag("Player");
            return;
        }

        transform.LookAt(Player.transform);
        
        // Move towards the player
        var target = Player.transform;
        var dir = target.position - transform.position;
        // Add force to this rigidbody to move it towards the target if the max speed isn't exceeded
        if (_rigidbody.velocity.magnitude< MaxSpeed)
        {
            _rigidbody.AddForce(dir.normalized * Acceleration * Time.deltaTime * 1000);
        }
        
    }
}
