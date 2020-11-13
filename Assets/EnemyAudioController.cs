using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class EnemyAudioController : MonoBehaviour
{
    AudioSource audioSource;
    public AudioClip[] audioClips;
    // Start is called before the first frame update
    private void Awake()
    {
        audioSource = GetComponent<AudioSource>();
    }
    public void PlayAudio(int index)
    {
        if (index < audioClips.Length)
        {
            audioSource.clip = audioClips[index];
            audioSource.Play();

        }
    }
}
