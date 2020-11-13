using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerStats : CharacterStats
{
    public event Action<int,Consumable> BuffPercent;

   public  void OnEquipmentChanged(Equipment newItem,Equipment oldItem)
    {
        if (newItem != null)
        {
            armor.AddModifier(newItem.armorModifier);
        damage.AddModifier(newItem.damageModifier);
        }
        if (oldItem != null)
        {
            armor.RemoveModifier(oldItem.armorModifier);
            damage.RemoveModifier(oldItem.damageModifier);
        }
    }
    //使用消耗品的回调
    public void OnUseConsumable(Consumable consumable)
    {
        if(consumable.Type == ConsumablesType.Hp)
        {
            //每秒加血
            StartCoroutine(AddHealth(consumable));
        }
        if(consumable.Type == ConsumablesType.Armour)
        {
            //加防御持续一定时间
            StartCoroutine(AddArmour(consumable));
      
        }
        if (consumable.Type == ConsumablesType.Damage)
        {
            //加伤害持续时间
            StartCoroutine(AddDamage(consumable));
        }

    }
    //回血协程
    IEnumerator AddHealth(Consumable consumable)
    {
        for (int i = 0; i < consumable.KeepTime; i++)
        {
            if(currentHealth<maxHealth)
            currentHealth += consumable.IncreaseNumber/consumable.KeepTime;
            //调用buff加血事件
            BuffPercent?.Invoke(i,consumable);
            yield return new WaitForSeconds(1);
        }
        
    }
 

    //加伤害药水使用协程
    IEnumerator AddDamage(Consumable consumable)
    {
        
        damage.AddModifier(consumable.IncreaseNumber);
        for (int i = 0; i < consumable.KeepTime; i++)
        {
            
            //调用加伤害buff事件
            BuffPercent?.Invoke(i, consumable);
            yield return new WaitForSeconds(1);
        }
        //移除伤害药水效果
        damage.RemoveModifier(consumable.IncreaseNumber);
    }
    //加防御药水使用协程
    IEnumerator AddArmour(Consumable consumable)
    {       
        armor.AddModifier(consumable.IncreaseNumber);
        for (int i = 0; i < consumable.KeepTime; i++)
        {
            //调用加防御事件
            BuffPercent?.Invoke(i, consumable);
            yield return new WaitForSeconds(1);
        }
        //移除防御药水效果
        armor.RemoveModifier(consumable.IncreaseNumber);
    }
    public override void Die()
    {
        base.Die();
        PlayerManager.instance.KillPlayer();
    }
}
