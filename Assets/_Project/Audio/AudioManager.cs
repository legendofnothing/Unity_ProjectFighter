using UnityEngine;
using System.Collections;
using System.Collections.Generic;

public class AudioManager : MonoBehaviour
{
    #region Unity Methods
    public static AudioManager instance { get; private set; }

    private void Awake() {
        if (instance != null && instance != this) {
            Destroy(this);
        }

        else
            instance = this;
    }

    void Start() {
        
    }
 
    void Update() {
        
    }
    #endregion

    public void PlaySoundEffect(AudioSource audioSource, AudioClip clip, float volume) {
        audioSource.PlayOneShot(clip, volume);
    }

    public void PlaySoundTrack(AudioSource audioSource, AudioClip clip, float volume) {
        audioSource.volume = volume;
        audioSource.loop = true;

        audioSource.clip = clip;

        audioSource.Play();
    }
}