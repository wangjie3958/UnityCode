using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CharacterCombat : MonoBehaviour
{
    public float attckSpeed = 1f;
    public float attackCooldowm = 0f;
    //攻击姿势保持的时间
     float combatCooldown = 5;
        float lastAttackTime;
    CharacterStats opponentStats;
    public bool InCombat { get; private set; }
    public event System.Action OnAttack;
    CharacterAnimationReciver animationReciver;
    CharacterStats myStats;
    private void Start()
    {
        myStats = GetComponent<CharacterStats>();
        animationReciver = GetComponentInChildren<CharacterAnimationReciver>();
    }
    private void Update()
    {
        attackCooldowm -= Time.deltaTime;
        if (Time.time - lastAttackTime > combatCooldown)
        {
            InCombat = false;
        }
    }
    public void Attack(CharacterStats targetStats)
    {
        if (attackCooldowm <= 0f&& !animationReciver.isSkillStart)
        {
            opponentStats = targetStats;
            //targetStats.TakeDamage(myStats.damage.GetValue());
            OnAttack?.Invoke();
            attackCooldowm = 1f / attckSpeed;
            InCombat = true;
            lastAttackTime = Time.time;
        }
    }


    public void AttackHit_AnimationEvent()
    {
        opponentStats.TakeDamage(myStats.damage.GetValue());
        if (opponentStats.currentHealth <= 0)
        {
            InCombat = false;
        }
    }
}
