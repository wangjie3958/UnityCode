using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerSkillController : MonoBehaviour
{
    //技能冷却
    public float QCoolingDelay = 2f;
    private float QLastTime;
    public float WCoolingDelay =2f;
    private float WLastTime;
    public float ECoolingDelay = 2f;
    private float ELastTime;
    //技能冷却回调
    public event Action<float,float,float> OnSkillDelayChanged;
   
    public AudioSource audioSource;
    CharacterAnimationReciver animationReciver;
    //动画控制器
     Animator PlayerAnimator;
    // Start is called before the first frame update
    void Start()
    {
        PlayerAnimator = GetComponentInChildren<Animator>();
        animationReciver = GetComponentInChildren<CharacterAnimationReciver>();
    }

    // Update is called once per frame
    void Update()
    {
        if (OnSkillDelayChanged != null)
        {
            float q = QLastTime==0?1:Mathf.Clamp01((Time.time - QLastTime) / QCoolingDelay);
            float w = WLastTime==0?1:Mathf.Clamp01((Time.time - WLastTime) / WCoolingDelay);
            float e = ELastTime==0?1:Mathf.Clamp01((Time.time - ELastTime) / ECoolingDelay);
            OnSkillDelayChanged.Invoke(q, w, e);
        }
        if (!animationReciver.isSkillStart)
        {
            //Q技能
            TryPlaySkill(KeyCode.Q, QCoolingDelay, QLastTime, "QSkill");
            //W
            TryPlaySkill(KeyCode.W, WCoolingDelay, WLastTime, "WSkill");
            //E
            TryPlaySkill(KeyCode.E, ECoolingDelay, ELastTime, "ESkill");
        }
    }

    private void TryPlaySkill(KeyCode code,float delay,float lastPlayTime,string skillNameTriggler)
    {
        if (Input.GetKeyDown(code))
        {
            if ((Time.time - lastPlayTime) > delay|| lastPlayTime==0f)
            {
                //释放技能..技能音效在动画帧事件中
                PlayerAnimator.SetTrigger(skillNameTriggler);
                if (code == KeyCode.Q)
                {
                    QLastTime = Time.time;
                }else if(code == KeyCode.W)
                {
                    WLastTime = Time.time;
                }
                else
                {
                    ELastTime = Time.time;
                }
            }
            else
            {
                //播放冷却中声音
                audioSource.Play();
            }
        }
    }

}
