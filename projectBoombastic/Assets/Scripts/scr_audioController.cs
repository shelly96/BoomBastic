using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Audio;

public class scr_audioController : MonoBehaviour
{

    public List<AudioClip> audioClips = new List<AudioClip>();
    public List<AudioSource> audioSources = new List<AudioSource>();

    // Start is called before the first frame update
    void Start()
    {
        foreach (AudioClip audioClip in audioClips) {
            AudioSource currentAudioSource = this.gameObject.AddComponent<AudioSource>();
            audioSources.Add(currentAudioSource);
            currentAudioSource.clip = audioClip;
        }
    }

    public void playSound(string audioClipName) {
        switch (audioClipName) {
            case "ambient":
                audioSources[0].playOnAwake = true;
                audioSources[0].volume = 0.3f;
                audioSources[0].loop = true;
                audioSources[0].Play();
                break;
            case "explosion":
                audioSources[1].playOnAwake = false;
                audioSources[1].volume = 0.2f;
                audioSources[1].loop = false;
                audioSources[1].Play();
                break;
            case "coin":
                audioSources[2].playOnAwake = false;
                audioSources[2].volume = 0.4f; 
                audioSources[2].loop = false;
                audioSources[2].Play();
                break;
            case "water":
                audioSources[3].playOnAwake = false;
                audioSources[3].volume = 0.2f;
                audioSources[3].loop = false;
                audioSources[3].Play();
                break;
            default:
                Debug.Log("Can't find audiofile");
                break;
        }
    }
}