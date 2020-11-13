using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class EnmetStats : CharacterStats
{
    EnemyController enemyController;
    float timer;
    bool isBackPoint =false;
    public int ReturnHPSpeed = 5;
    HealhUI healhUI;

    //携带的Item
    [Header("CarryHeader")]
    public Item[] carryItems;
    public  Animator animator;
    EnemyAudioController audioController;
    private void Awake()
    {
        enemyController = GetComponent<EnemyController>();
        enemyController.OnBackCallback += EachReturnHealth;
        timer = 0;
        healhUI = GetComponent<HealhUI>();
        currentHealth = maxHealth;
        audioController = GetComponent<EnemyAudioController>();
    }
    public override void Die()
    {
        base.Die();
        isDead = true;
        //移除rigbody
        Destroy(gameObject.GetComponent<Rigidbody>());
        //add Ragdoll effect /death  animation
        animator.SetTrigger("Die");
        //爆出装备
        foreach (var item in carryItems)
        {
            ItemManager.instance.CreateDiscordItem(transform.position, item);
        }
       
        //切换音乐
        MainBgmAudioManager.instance.SwichBgm(0);
        //任务完成
        TaskManager.instance.CompletedTask("击杀骷髅怪");
        //播放死亡音效
        audioController.PlayAudio(0);
    }
    private void Update()
    {
        if (isBackPoint)
        {
            timer += Time.deltaTime;
            if (timer >= 1)
            {
                currentHealth += ReturnHPSpeed;
                if (currentHealth >= maxHealth)
                {
                    currentHealth = maxHealth;
                    isBackPoint = false;
                }
                healhUI.OnHealthChanged(maxHealth, currentHealth);
                timer = 0;
            }
        }
    }

    //回家回血
    public void EachReturnHealth()
    {
        isBackPoint = true;
    }
}



