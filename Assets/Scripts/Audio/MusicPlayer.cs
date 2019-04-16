using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;
/// <summary>
/// Music player 
/// </summary>
public class MusicPlayer : MonoBehaviour
{
    [SerializeField] private AudioClip menuMusic, gameMusic;
    private AudioSource source;
    private bool firstLevelReached;

    void Start()
    {
        source = GetComponent<AudioSource>();

        if(SceneManager.GetActiveScene().name == "MainMenu") {
            firstLevelReached = false;
            source.clip = menuMusic;
            source.Play();
        }
        else {
            if (!firstLevelReached) {
                firstLevelReached = true;
                source.clip = gameMusic;
                source.Play();
            }
        }
    }

}
