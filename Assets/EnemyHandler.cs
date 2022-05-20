using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class EnemyHandler : MonoBehaviour
{
    [Header("Health and Damage")]
    public float MaxDamageDistance = 5f;
    public int Health = 10;

    public bool IsBoss = false;

    [Header("Scoring")]
    public int KillValue = 10;

    [Header("Sounds")]
    public AudioClip DamageSound;
    public AudioClip DeathSound;

    GameObject _player;
    public void TakeDamage(int damage)
    {

        Health -= damage;
        if (Health <= 0)
        {
            // Get the game controller to change global state
            GameObject gameController = GameObject.FindGameObjectWithTag("GameController");

            // The boss is dead, play the death sound
            if (DeathSound != null)
            {
                AudioSource.PlayClipAtPoint(DeathSound, transform.position);
            }

            // Increase the current score
            ScoreManager.Score += KillValue;

            // Handle switching to the next level if the enemy is a boss
            if (IsBoss)
            {

                //Find object with tag "GameController"
                gameController.GetComponent<GenerateTerrain>().StartNextLevel();
            }

            // Do this last because it stops this script
            Destroy(gameObject);
        }
        else
        {
            // Enemy not dead, play damage sound
            if (DamageSound != null)
            {
                AudioSource.PlayClipAtPoint(DamageSound, transform.position);
            }

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
