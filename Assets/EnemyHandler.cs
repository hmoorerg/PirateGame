using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class EnemyHandler : MonoBehaviour
{
    public float MaxDamageDistance = 5f;
    public int Health = 10;

    public AudioClip DamageSound;
    GameObject _player;

    public bool IsBoss = false;
    public void TakeDamage(int damage)
    {
        AudioSource.PlayClipAtPoint(DamageSound, transform.position, 1f);

        Health -= damage;
        if (Health <= 0)
        {

            if (IsBoss) 
            {
                //Find object with tag "GameController"
                GameObject gameController = GameObject.FindGameObjectWithTag("GameController");
                gameController.GetComponent<GenerateTerrain>().StartNextLevel();
            }
            
            // Do this last because it stops this script
            Destroy(gameObject);
        }
    }

    // Start is called before the first frame update
    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {
        // Get the player object
        _player = GameObject.FindGameObjectWithTag("Player");

        // Take damage if the player presses the z key
        if (Input.GetKeyDown(KeyCode.Z))
        {
            // Check if the player is in range
            if (Vector3.Distance(transform.position, _player.transform.position) < MaxDamageDistance)
            {
                // Take damage
                TakeDamage(1);
            }
        }
    }
}
