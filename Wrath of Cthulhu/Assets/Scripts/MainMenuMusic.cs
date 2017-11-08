using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class MainMenuMusic : MonoBehaviour {

    private AudioSource audioSource;
    public AudioSource[] sounds;
    private void Awake()
    {
        DontDestroyOnLoad(transform.gameObject);
        sounds = GetComponents<AudioSource>();
    }

    public void PlayMainMenuMusic()
    {
        sounds[1].Stop();
        sounds[0].Play();
    }

    public void PlayGameMusic()
    {
        sounds[0].Stop(); // stops main menu music
        sounds[1].Play(); // plays in-game music
    }

    public void stopGameAudio() //Mark calls this function if he dies
    {
        sounds[1].Stop();
    }

}
