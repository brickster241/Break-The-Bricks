using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using System;

// Singleton class for Managing Audio.
public class AudioManager : MonoBehaviour
{
    public static AudioManager Instance { get; private set;}
    [SerializeField] AudioInfo[] sounds;

    private void Awake() {
        if (Instance == null) {
            Instance = this;
            DontDestroyOnLoad(gameObject);
        } else {
            Destroy(gameObject);
        }

        foreach (AudioInfo soundInfo in sounds) {
            soundInfo.audioSource = gameObject.AddComponent<AudioSource>();
            soundInfo.audioSource.clip = soundInfo.audioClip;
            soundInfo.audioSource.volume = soundInfo.volume;
            soundInfo.audioSource.loop = soundInfo.loop;
        }
    }

    // Play Audio of Given AudioType.
    public void PlayAudio(AudioType audioType) {
        AudioInfo soundInfo = Array.Find(sounds, item => item.audioType == audioType);
        soundInfo.audioSource.Play();
    }

    // Stop Audio of Given AudioType.
    public void StopAudio(AudioType audioType) {
        AudioInfo soundInfo = Array.Find(sounds, item => item.audioType == audioType);
        soundInfo.audioSource.Stop();
    }
    
}
