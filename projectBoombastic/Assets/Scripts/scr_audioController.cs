using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Audio;

public class scr_audioController : MonoBehaviour
{

    public List<AudioClip> audioClips = new List<AudioClip>();
    private List<AudioSource> audioSources = new List<AudioSource>();

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
                audioSources[0].volume = 0.2f;
                audioSources[0].loop = true;
                audioSources[0].Play();
                break;
            case "ambient_birds":
                audioSources[1].playOnAwake = true;
                audioSources[1].volume = 0.1f;
                audioSources[1].loop = true;
                audioSources[1].Play();
                break;  
            case "explosion":
                audioSources[2].playOnAwake = false;
                audioSources[2].volume = 0.3f;
                audioSources[2].loop = false;
                audioSources[2].Play();
                break;
            case "coin":
                audioSources[3].playOnAwake = false;
                audioSources[3].volume = 0.4f; 
                audioSources[3].loop = false;
                audioSources[3].Play();
                break;
            case "water":
                audioSources[4].playOnAwake = false;
                audioSources[4].volume = 0.2f;
                audioSources[4].loop = false;
                audioSources[4].Play();
                break;
            case "wood":
                audioSources[5].playOnAwake = false;
                audioSources[5].volume = 0.3f;
                audioSources[5].loop = false;
                audioSources[5].Play();
                break;
            case "jump":
                audioSources[6].playOnAwake = false;
                audioSources[6].volume = 0.4f;
                audioSources[6].loop = false;
                audioSources[6].pitch = 1.1f;
                audioSources[6].Play();
                break;
            case "hit":
                audioSources[7].playOnAwake = false;
                audioSources[7].volume = 0.4f;
                audioSources[7].loop = false;
                audioSources[7].pitch = 1.2f;
                audioSources[7].Play();
                break;
            case "gameOver":
                audioSources[8].playOnAwake = false;
                audioSources[8].volume = 0.4f;
                audioSources[8].loop = false;
                audioSources[8].Play();
                break;
            case "woodUI":
                audioSources[9].playOnAwake = false;
                audioSources[9].volume = 0.4f;
                audioSources[9].loop = false;
                audioSources[9].Play();
                break;
            case "heal":
                audioSources[10].playOnAwake = false;
                audioSources[10].volume = 0.4f;
                audioSources[10].loop = false;
                audioSources[10].Play();
                break;
            default:
                Debug.Log("Can't find audiofile");
                break;
        }
    }
}
