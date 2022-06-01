using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CoinSpawner : MonoBehaviour 
{
    // Static instance for use in other scripts
    public static CoinSpawner Instance;

    public GameObject coinPrefab;

    public void SpawnCoins(Vector3 spawnPosition, int coinValue) {
        GameObject coin = Instantiate(coinPrefab, spawnPosition, Quaternion.identity);
    }
}
