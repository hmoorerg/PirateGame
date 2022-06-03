using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using System.Linq;

public class CoinManager : MonoBehaviour
{
    // The list of coin prefabs that this can spawn
    public List<GameObject> coinPrefabs;

    public void SpawnAt(Vector3 position, int value)
    {
        // Keep spawning coins until we have reached the value
        while (value > 0)
        {
            // Get the first coin that is under the target value:
            var largestCoin = coinPrefabs
                .OrderByDescending(x => x.GetComponent<CoinFollow>().PointValue)
                .FirstOrDefault(x => x.GetComponent<CoinFollow>().PointValue <= value);

            // Stop the logic if we don't have a valid coin to spawn
            if (largestCoin == null) 
            {
                break;
            }

            // Spawn the coin at the position
            Instantiate(largestCoin, position, Quaternion.identity);

            // Decrease the value by the coin's value
            value -= largestCoin.GetComponent<CoinFollow>().PointValue;
            
        }

        
        // Pick a random coin prefab
        GameObject coin = coinPrefabs[Random.Range(0, coinPrefabs.Count)];

        // Spawn the coin at the given position
        Instantiate(coin, position, Quaternion.identity);
    }

    // Start is called before the first frame update
    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {
        
    }
}
