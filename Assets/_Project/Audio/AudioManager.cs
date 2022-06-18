using UnityEngine;
using System.Collections;
using System.Collections.Generic;

public class AudioManager : MonoBehaviour
{
    public static AudioManager manager { get; private set; }

    public AudioSource audioSource;

    private void Awake() {
        if (manager != null && manager != this) {
            Destroy(this);
        }

        else
            manager = this;
    }

    private void Start() {
        audioSource = GetComponent<AudioSource>();
    }

    public void PlaySFX(AudioClip clip, float volume) {
        audioSource.PlayOneShot(clip, volume);
    }

    public void PlayTrack(AudioClip track, float volume) {
        audioSource.clip = track;
        audioSource.volume = volume;

        audioSource.Play();
    }
}