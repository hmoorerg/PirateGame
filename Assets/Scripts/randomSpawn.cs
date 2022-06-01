using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class randomSpawn : MonoBehaviour
{
	public GameObject[] enemies;
    // Start is called before the first frame update
    void Start()
    {
		int randomenemy = Random.Range(0,enemies.Length);
        Instantiate(enemies[randomenemy], transform.position, Quaternion.identity);
    }

}
