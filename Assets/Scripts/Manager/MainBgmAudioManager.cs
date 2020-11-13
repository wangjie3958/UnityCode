using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class MainBgmAudioManager : MonoBehaviour
{
    public static MainBgmAudioManager instance;
    public AudioClip[] BgmAudioDatas;
     EnemyController enemyController;
   public  AudioSource audioSource;
    
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
        DontDestroyOnLoad(gameObject);
    }
    public void Init()
    {
        if (SceneManager.GetActiveScene().name == SceneEnum.Game.ToString())
        {
           instance.SwichBgm(0);
            //初始化玩家靠近敌人的BGM回调函数
            enemyController = FindObjectOfType<EnemyController>();
            enemyController.EnterDetectionRange += EnterDetectionRange;
            enemyController.ExitDetectionRange += ExitDetectionRange;
        }
        else if(SceneManager.GetActiveScene().name == SceneEnum.FoundSword.ToString())
        {
            instance.SwichBgm(2);
        }

    }
    // Update is called once per frame

    //切换背景音乐
    public void SwichBgm(int index)
    {
        audioSource.clip = BgmAudioDatas[index];
        audioSource.Play();
    }
    //进入战斗音效的事件
    public void EnterDetectionRange()
    {
        SwichBgm(1);
    }
    //退出战斗场景切换事件
    public void ExitDetectionRange()
    {
        SwichBgm(0);
    }
}
