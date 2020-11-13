using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class SoundEffectManager : MonoBehaviour
{
    public static SoundEffectManager instance;
    public AudioSource SoundEffectSouce;
    //各种音效
  public   AudioClip[] soundEffectAudioClips;
    // Start is called before the first frame update
    private void Awake()
    {
        if (instance == null)
        {
            instance = this;
        }
        else
        {
            Destroy(gameObject);
        }
    }
    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {
        
    }
    //播放音效
    public void PlaySoundEffect(int index)
    {
        SoundEffectSouce.clip = soundEffectAudioClips[index];
        SoundEffectSouce.Play();
    }
}
