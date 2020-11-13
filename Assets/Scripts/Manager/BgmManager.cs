using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class BgmManager : MonoBehaviour
{
    public static BgmManager instance;
    public AudioClipDatas ClipDatas;
    AudioSource audioSource;
    // Start is called before the first frame update
    private void Awake()
    {
         instance = this;
        audioSource = GetComponent<AudioSource>();

    }
    void Start()
    {
        //背景音乐播放
        audioSource.clip = ClipDatas.audioClips[0];
        audioSource.Play();
    }



}
