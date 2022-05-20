using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using System.Diagnostics;

public class DieAfterTime : MonoBehaviour
{
    public float SecondsUntilDeath = 100f;

    private Stopwatch _stopWatch = new Stopwatch();

    // Start is called before the first frame update
    void Start()
    {
        _stopWatch.Start();
    }

    // Update is called once per frame
    void Update()
    {
        // Check if the time is up
        if (_stopWatch.Elapsed.TotalSeconds > SecondsUntilDeath)
        {
            Destroy(gameObject);
        }
    }
}
