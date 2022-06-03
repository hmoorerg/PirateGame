using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class RestartGameButton : MonoBehaviour
{
    // Start is called before the first frame update
    void Start()
    {
        // Add a listener to the OnClick event
        GetComponent<Button>().onClick.AddListener(() => {
            // Reload the level
            UnityEngine.SceneManagement.SceneManager.LoadScene("Procedural");
        });
    }
}
