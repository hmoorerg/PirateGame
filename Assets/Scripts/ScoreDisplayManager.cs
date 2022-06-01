using System.Collections;
using System.Collections.Generic;
using TMPro;
using UnityEngine;
using UnityEngine.UI;

public class ScoreDisplayManager : MonoBehaviour
{
    // Start is called before the first frame update
    private TextMeshProUGUI _textObject;

    void Start()
    {
        _textObject = GetComponent<TextMeshProUGUI>();
    }

    // Update is called once per frame
    void Update()
    {
        _textObject.text = $"${ScoreManager.Score}";
    }
}
