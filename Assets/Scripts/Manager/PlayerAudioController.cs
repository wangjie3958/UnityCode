using System.Collections;
using System.Collections.Generic;
using System.Linq;
using UnityEngine;
using UnityEngine.AI;

public class PlayerAudioController : MonoBehaviour
{
    public AudioClip punchAttack;
    public AudioClip swordAttack;
    public AudioSource attackAudioSource;

    public AudioClip bodyTakeDamage;
    public AudioClip armorTakeDamage;
    public AudioSource takeDamageSoure;

    public AudioClip footStep;
    public AudioClip[] SkillSound;
    public AudioSource footStepSource;
    public bool isStartSkill = false;


    AudioClip  currentTakeDamageAudioClip;
    AudioClip currentAttackAudioClip;
    CharacterCombat combat;
    CharacterStats stats;
    NavMeshAgent agent;
    bool isRun = false;
    float currentRunSpeed;

    private void Awake()
    {
        currentAttackAudioClip = punchAttack;
        combat = GetComponent<CharacterCombat>();
        stats = GetComponent<CharacterStats>();
        agent = GetComponent<NavMeshAgent>();
        combat.OnAttack += AttackAudio;

        stats.OnTakeDamage += OnTakeDamage;
        footStepSource.clip = footStep;
    }
    private void Update()
    {
        if (!isStartSkill)
        {
            currentRunSpeed = agent.velocity.magnitude;
            footStepSource.pitch = (currentRunSpeed / agent.speed);
            if (!isRun && agent.velocity.magnitude >= 0.1)
            {
                isRun = true;
                footStepSource.Play();
            }
            else if (isRun && agent.velocity.magnitude < 0.1)
            {
                isRun = false;
                footStepSource.Stop();
            }
        }
        else
        {
            isRun = false;
        }
    }
    private void OnTakeDamage(int damage)
    {
        if (damage >= 5)
        {
            currentTakeDamageAudioClip = bodyTakeDamage;
        }
        else
        {
            currentTakeDamageAudioClip = armorTakeDamage;
        }
        takeDamageSoure.clip = currentTakeDamageAudioClip;
        takeDamageSoure.Play();
    }

    private void AttackAudio()
    {
        attackAudioSource.clip = currentAttackAudioClip;

        attackAudioSource.Play();
    }

    public void OnEquipmentChanged(Equipment newItem,Equipment oldItem)
    {
        if (EquiomentManager.instance.currentEquipment.Where(e => e != null && e.equipSlot == EquipmentSlot.Weapon).Count()>0)
        {
            currentAttackAudioClip = swordAttack;
        }
        else
        {
            currentAttackAudioClip = punchAttack;
        }
        if (newItem != null && newItem.equipSlot == EquipmentSlot.Weapon)
        {
            currentAttackAudioClip = swordAttack;
        }
    }
    //初始化不同装备产生不同的声音
    public void OnEquipmentInit(Equipment[] equipments)
    {

            if (equipments.Where(e => e != null && e.equipSlot == EquipmentSlot.Weapon).Count() > 0)
            {
                currentAttackAudioClip = swordAttack;
            }
            else
            {
                currentAttackAudioClip = punchAttack;
            }


    }

    //技能按下帧事件
    public void SkillStartEvent(string skill)
    {
        isStartSkill = true;
        if (skill.Equals("Q"))
        {
            footStepSource.clip = SkillSound[0];
            footStepSource.pitch = 1;
            footStepSource.loop = false;
        }
        else if (skill.Equals("W"))
        {
            footStepSource.clip = SkillSound[1];
            footStepSource.pitch = 1;
        }
        else
        {
            footStepSource.clip = SkillSound[2];
            footStepSource.pitch = 1;
            footStepSource.loop = false;
        }
        footStepSource.Play();
    }
    //技能结束帧事件
    public void SkillEndEvent()
    {
        isStartSkill = false;
        footStepSource.clip = footStep;
        footStepSource.loop = true;
    }

}
