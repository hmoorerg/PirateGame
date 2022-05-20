using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using TMPro;

public class LevelIndicatorManager : MonoBehaviour
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
        _textObject.text = $"Level {WorldSettings.CurrentLevelNumber}";
    }
}
