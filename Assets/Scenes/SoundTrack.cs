using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class SoundTrack : MonoBehaviour
{
    public AudioClip trackToPlay;

    private void Start() {
        AudioManager.instance.PlaySoundTrack(trackToPlay, 0.5f);
    }
}
