using UnityEngine;
using TMPro;
using System.Diagnostics;

public class TimerIndicatorManager : MonoBehaviour
{
    private Stopwatch _stopWatch = new Stopwatch();
    private TextMeshProUGUI _textObject;
    // Start is called before the first frame update
    void Start()
    {
        _stopWatch.Start();
        _textObject = GetComponent<TextMeshProUGUI>();
    }

    // Update is called once per frame
    void Update()
    {
        var timespan = _stopWatch.Elapsed;

        _textObject.text = timespan.ToString(@"ss\:ff");
    }
}
