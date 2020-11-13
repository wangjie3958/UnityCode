using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Audio;

public class UIAudioController : MonoBehaviour
{
   public  AudioMixer audioMixer;

    public AudioClipDatas UIAudioClips;
    public AudioSource audioSource;
    public void PlayAudio(int index)
    {
        audioSource.clip = UIAudioClips.audioClips[index];
        audioSource.Play();
   }
    //main音量调节
    public void  MainSoundStrengthSet(float value)
    {
        audioMixer.SetFloat("Main", value);
    }

    //BGM音乐调节
    public void  BgmStrengthSet(float value)
    {
        audioMixer.SetFloat("Bgm", value);
    }
    //SoundEffect调节
    public void  EffectSoundStrengthSet(float value)
    {
        audioMixer.SetFloat("SoundEffect", value);
    }



}
