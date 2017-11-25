using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class MainMenuMusic : MonoBehaviour {

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
        FadeOutMusic(sounds[0]); // stops main menu music
        //sounds[0].Stop();
        //sounds[1].Play(); // plays in-game music
    }

    public void PlayBossMusic()
    {
        sounds[2].Play();
    }

    public void stopGameAudio() //Mark calls this function if he dies
    {
        sounds[1].Stop();
        sounds[2].Stop();
    }

    public void FadeOutMusic(AudioSource source)
    {
        StartCoroutine(FadeMusic(source));
    }
    IEnumerator FadeMusic(AudioSource source)
    {
        while (source.volume > .01F)
        {
            source.volume = Mathf.Lerp(source.volume, 0F, Time.deltaTime * 1.5f);
            yield return 0;
        }
        source.volume = 0;
        sounds[1].Play();
        //perfect opportunity to insert an on complete hook here before the coroutine exits.
    }

}
