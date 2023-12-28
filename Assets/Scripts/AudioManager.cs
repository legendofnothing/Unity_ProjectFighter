using UnityEngine;
using System.Collections;
using System.Collections.Generic;

public class AudioManager : MonoBehaviour
{
    #region Unity Methods
    public static AudioManager instance { get; private set; }

    public AudioSource audioSource;

    private void Awake() {
        if (instance != null && instance != this) {
            Destroy(this);
        }

        else
            instance = this;
    }

    void Start() {
        audioSource = GetComponent<AudioSource>();

        if(audioSource == null) {
            Debug.Log("AudioSource on AudioManager is null");
        }
    }
 
    void Update() {
        
    }
    #endregion

    public void PlaySoundEffect(AudioClip clip, float volume) {
        audioSource.PlayOneShot(clip, volume);
    }

    public void PlaySoundTrack( AudioClip clip, float volume) {
        audioSource.volume = volume;
        audioSource.loop = true;

        audioSource.clip = clip;

        audioSource.Play();
    }
}