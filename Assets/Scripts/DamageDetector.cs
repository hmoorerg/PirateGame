using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class DamageDetector : MonoBehaviour
{
    public GameObject _player;

    void OnCollisionEnter2D(Collision2D collision)
    {
        // Check if the collided object is of tag "Enemy"
        if (collision.gameObject.tag == "Enemy")
        {
            // Get player object
            var playerController = _player.GetComponent<PlayerController>();

            // Decrease player's health
            playerController.Health -= 1;
        }
    }


    // Start is called before the first frame update
    void Start()
    {
        _player = GameObject.FindGameObjectWithTag("Player");
    }

    // Update is called once per frame
    void Update()
    {
        
    }
}
