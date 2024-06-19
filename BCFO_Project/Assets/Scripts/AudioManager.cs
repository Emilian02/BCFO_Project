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

    public void Awake()
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

    public void Start()
    {
        if(sceneName == "Menu")
        {
            PlayMusic("Menu");
        }
        else if (sceneName == "Map")
        {
            PlayMusic("Map");
        }
        else if(sceneName == "Level 1")
        {
            PlayMusic("Level 1");
        }
        else if(sceneName == "Level 2")
        {
            PlayMusic("Level 2");
        }
        else if (sceneName == "Level 3")
        {
            PlayMusic("Level 3");
        }
        else if (sceneName == "Boss Level")
        {
            PlayMusic("Boss Level");
        }
        else
        {
            Debug.Log("Missing Scene Name");
        }

    }


    public void PlayMusic(string  name)
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
    
   public void PlaySFX(String name)
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
}
