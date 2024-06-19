using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using System;

public class AudioManager : MonoBehaviour
{
    public static AudioManager instance;

    public string sceneName;

    public Sound[] musicSounds, sfxSounds;
    public AudioSource musicSource, sfxSource;

    private void Awake()
    {
        if (instance == null)
        {
            instance = this;
            DontDestroyOnLoad(gameObject);
        }
        else
        {
            Destroy(gameObject);
        }
    }

    private void Start()
    {
        PlaySceneMusic(sceneName);
    }

    private void PlaySceneMusic(string sceneName)
    {
        switch (sceneName)
        {
            case "Menu":
                PlayMusic("Menu");
                break;
            case "Map":
                PlayMusic("Map");
                break;
            case "Level 1":
                PlayMusic("Level 1");
                break;
            case "Level 2":
                PlayMusic("Level 2");
                break;
            case "Level 3":
                PlayMusic("Level 3");
                break;
            case "Boss Level":
                PlayMusic("Boss Level");
                break;
            default:
                Debug.Log("Missing Scene Name");
                break;
        }
    }

    public void PlayMusic(string name)
    {
        Sound s = Array.Find(musicSounds, x => x.name == name);

        if (s == null)
        {
            Debug.Log("SOUND NOT FOUND");
        }
        else
        {
            musicSource.clip = s.clip;
            musicSource.Play();
        }
    }

    public void PlaySFX(string name)
    {
        Sound s = Array.Find(sfxSounds, x => x.name == name);

        if (s == null)
        {
            Debug.Log("SOUND NOT FOUND");
        }
        else
        {
            sfxSource.PlayOneShot(s.clip);
        }
    }

    // Methods to change volume
    public void SetMusicVolume(float volume)
    {
        musicSource.volume = volume;
    }

    public void SetSFXVolume(float volume)
    {
        sfxSource.volume = volume;
    }

    public void SetGeneralVolume(float volume)
    {
        musicSource.volume = volume;
        sfxSource.volume = volume;
    }
}
