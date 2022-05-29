using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class RandomMusicPlayer : MonoBehaviour
{

    public AudioClip[] Songs;
    private AudioSource _audioSource;
    // Start is called before the first frame update
    void Start()
    {
        _audioSource = GetComponent<AudioSource>();
        
    }

    // Update is called once per frame
    void Update()
    {
        if (!_audioSource.isPlaying) 
        {

            PlayRandomSong();
        }
        
    }
    
    void PlayRandomSong()
    {
        var randomSong = Songs[Random.Range(0,Songs.Length)];

        _audioSource.clip = randomSong; 
        _audioSource.Play();
    }
}
