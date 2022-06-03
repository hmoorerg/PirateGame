using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Chest : MonoBehaviour
{

    // The gameobject to spanw when the chest is opened
    public GameObject ItemToSpawn;

    public int Price = 30;

    // Start is called before the first frame update
    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {
        
    }

    public void OpenChest()
    {
        // Spawn the item
        Instantiate(ItemToSpawn, transform.position + (Vector3.up * 2), Quaternion.identity);

        // Destroy the chest
        Destroy(gameObject);
    }

    // Detect when the player is in the trigger
    private void OnTriggerEnter2D(Collider2D other)
    {
        if (other.gameObject.tag == "Player" && ScoreManager.Score >= Price)
        {
            ScoreManager.Score -= Price;
            // Open the chest
            OpenChest();
        }
    }
}
