using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class DespawnWithDistance : MonoBehaviour
{
    public GameObject Player;
    public float MaxDistance = 50f;

    // Start is called before the first frame update
    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {
        if (Player == null)
        {
            Player = GameObject.FindWithTag("Player");
        }

        // Get the distance between the player and the object
        var distance = Vector3.Distance(transform.position, Player.transform.position);

        // Check if the distance is greater than the max distance
        if (distance > MaxDistance)
        {
            // Destroy the object
            Destroy(gameObject);
        }
        
    }
}
