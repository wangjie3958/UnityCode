using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CharacterAnimationReciver : MonoBehaviour
{
    public CharacterCombat combat;
    public PlayerAudioController PlayerAudioController;
    public GameObject fireBall;
    public Transform fireBallPosition;
    public bool isSkillStart = false;

    public GameObject DeadDestoryGo;
 public void AttackHitEvent()
    {
        combat.AttackHit_AnimationEvent();
    }

    //Q技能播放
    public void QSkillStart()
    {
        //播放Q音效
        PlayerAudioController.SkillStartEvent("Q");
        isSkillStart = true;
    }

    //W技能播放
    public void WSkillStart()
    {
        PlayerAudioController.SkillStartEvent("W");
        isSkillStart = true;
    }

    //E技能播放
    public void ESkillStart()
    {
        isSkillStart = true;
    }
    //技能停止
    public void SkillStop()
    {
        PlayerAudioController.SkillEndEvent();
        isSkillStart = false;
    }
    //火球发出
    public void FireBall()
    {
        PlayerAudioController.SkillStartEvent("E");
        GameObject fireball= Instantiate(fireBall);
        fireball.transform.position = fireBallPosition.position;
        fireball.transform.rotation = fireBallPosition.rotation;
    }
    //骷髅死亡删除
    public void Delete()
    {
        Destroy(DeadDestoryGo);
    }
}
