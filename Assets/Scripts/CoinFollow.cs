using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CoinFollow : MonoBehaviour
{
    private GameObject _player;
    
    [Header("Movement")]
    public float MaxSpeed = 5f;
    public float Acceleration = 100f;
    public float MaxFollowDistance = 4f;
    public float SecondsUntilFollowingStarts = 2f;

    [Header("Value")]
    public int PointValue = 5;

    [Header("Sounds")]
    public AudioClip[] CoinPickupSounds;

    private Rigidbody2D _rigidbody;

    private float _timeSinceCreation = 0f;


    // Destroys itself like a coin
    void OnCollisionEnter2D(Collision2D collision)
    {
        // Check if the collided object is the player
        if (collision.gameObject.tag == "Player")
        {
            // Increases the player's score
            ScoreManager.Score += PointValue;

            if (CoinPickupSounds.Length != 0) 
            {
                // Play a random coin pickup sound
                var sound = CoinPickupSounds[Random.Range(0, CoinPickupSounds.Length)];
                AudioSource.PlayClipAtPoint(sound, transform.position, 0.5f);
            }


            // Destroy this object
            Destroy(gameObject);
        }
    }


    // Start is called before the first frame update
    void Start()
    {
        _rigidbody = GetComponent<Rigidbody2D>();
    }

    // Update is called once per frame
    void Update()
    {
        if (_timeSinceCreation < SecondsUntilFollowingStarts)
        {
            _timeSinceCreation += Time.deltaTime;
            
            // Don't follow the player yet
            return;
        }


        if (_player == null) 
        {
            _player = GameObject.FindGameObjectWithTag("Player");
            return;
        }

        if (Vector3.Distance(transform.position, _player.transform.position) <= MaxFollowDistance)
        {
            // Move towards the player
            var target = _player.transform;
            var dir = target.position - transform.position;

            // Get the object's current velocity in the target direction
            var currentVelocity = Vector3.Dot(dir.normalized, _rigidbody.velocity);

            // Add force to this rigidbody to move it towards the target if the max speed isn't exceeded
            if (currentVelocity < MaxSpeed)
            {
                // Add force to this rigidbody to move it towards the target if the max speed isn't exceeded
                _rigidbody.AddForce(dir.normalized * Acceleration * Time.deltaTime * 1000);
            }
        }
        
    }
}
