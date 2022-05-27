﻿using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using System.Threading.Tasks;

public class EnemyHandler : MonoBehaviour
{
    [Header("Health and Damage")]
    public float MaxDamageDistance = 5f;
    public int Health = 10;

    public bool IsBoss = false;

    [Header("Scoring")]
    public GameObject CoinPrefab;
    public int MinCoinReward = 1;
    public int MaxCoinReward = 10;

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
            var randomCoinAmount = Random.Range(MinCoinReward, MaxCoinReward);

            // Handle switching to the next level if the enemy is a boss
            if (IsBoss)
            {
                // No time to collect coins for bosses so just give it to the player
                ScoreManager.Score += randomCoinAmount;
                //Find object with tag "GameController"
                gameController.GetComponent<GenerateTerrain>().StartNextLevel();
            } 
            else 
            {
                // The enemy is a regular enemy, release coins
                for (int i = 0; i < randomCoinAmount; i++)
                {
                    // Create a coin at this enemy's position
                    var coin = Instantiate(CoinPrefab, transform.position, Quaternion.identity);
                    // Add a random force to the coin
                    coin.GetComponent<Rigidbody2D>().AddForce(new Vector2(Random.Range(-1f, 1f), Random.Range(1f, 2f)) * 10);
                }
            }

            // Do this last because it stops this script
            this.gameObject.GetComponent<GroundFollow>().isAlive = false;
            
            Destroy(gameObject,1);
        }
        else
        {
            // The enemy survived, push it away
            PushAwayFromPlayer(1000f);

            // Enemy not dead, play damage sound
            if (DamageSound != null)
            {
                AudioSource.PlayClipAtPoint(DamageSound, transform.position);
            }

        }
    }

    void PushAwayFromPlayer(float force) 
    {
        // Get a unit vector facing away from the player
        Vector3 direction = (transform.position - _player.transform.position).normalized;

        // Apply force in the direction away from the player
        gameObject.GetComponent<Rigidbody2D>().AddForce(direction * force);
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

    }
}
