using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class SetRandomSprite : MonoBehaviour
{
    public Sprite[] Sprites;
    // Start is called before the first frame update
    void Start()
    {
        var randomSprite = Sprites[Random.Range(0,Sprites.Length)];
        GetComponent<SpriteRenderer>().sprite = randomSprite;
    }
}
