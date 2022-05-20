using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class EnemyHandler : MonoBehaviour
{
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
            this.gameObject.GetComponent<GroundFollow>().isAlive = false;
            
            Destroy(gameObject,1);
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

    }
}
