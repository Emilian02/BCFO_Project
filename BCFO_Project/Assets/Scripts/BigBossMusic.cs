using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class BigBossMusic : MonoBehaviour
{
    [Header("AUDIO")]
    public AudioClip startAudio;
    public AudioClip bossAudio;
    public AudioClip bonkAudio;
    AudioSource audioPlay;
    AudioSource sfxPlay;
    bool hasPlayed = false;
    void Start()
    {
        audioPlay = GetComponent<AudioSource>();
        sfxPlay = GetComponent<AudioSource>();
    }

    void OnTriggerEnter2D(Collider2D collision)
    {
        if (collision.tag == "Player" && hasPlayed == false)
        {
            audioPlay.PlayOneShot(startAudio, 0.08f);
            Invoke(nameof(Bonk), 1.4f);
            Invoke(nameof(StopStartMusic), 2.3f);
            Invoke(nameof(StartBossMusic), 2.4f);
        }
    }

    void StopStartMusic()
    {
        audioPlay.Stop();
    }
    void StartBossMusic()
    {
        audioPlay.PlayOneShot(bossAudio, 0.1f);
        hasPlayed = true;
    }
    void Bonk()
    {
        sfxPlay.PlayOneShot(bonkAudio, 0.09f);
    }
}
