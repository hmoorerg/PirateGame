using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using TMPro;

public class HealthIndicatorManager : MonoBehaviour
{
    private TextMeshProUGUI _textObject;
    // Start is called before the first frame update
    void Start()
    {
        _textObject = GetComponent<TextMeshProUGUI>();
    }

    // Update is called once per frame
    void Update()
    {
        // Get player object
        GameObject player = GameObject.FindGameObjectWithTag("Player");
        var playerController = player.GetComponent<PlayerController>();
        
        _textObject.text = $"Health: {((double)playerController.Health / (double)playerController.MaxHealth)*100}%";

        
    }
}
