using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ExploreAudio : MonoBehaviour
{
    AudioSource exploreMainMusic;
    void Start()
    {
        exploreMainMusic = GetComponent<AudioSource>();
        exploreMainMusic.volume = PlayerPrefs.GetFloat("MusicVolume");
        exploreMainMusic.Play();
    }

    // Update is called once per frame
    void Update()
    {
        
    }
}
