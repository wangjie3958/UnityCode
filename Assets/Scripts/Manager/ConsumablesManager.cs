using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ConsumablesManager : MonoBehaviour
{
    public static ConsumablesManager instance;
    [HideInInspector]
    public PlayerStats stats;
    public event Action<Consumable> OnUseConsumableCallback;
    private void Awake()
    {
        if (instance == null)
        {
            instance = this;
        }
        else
        {
            Destroy(this);
        }
    }
     public void Init()
    {
       //初始化玩家的使用物品的回调
        stats = GameObject.FindGameObjectWithTag("Player").GetComponent<PlayerStats>();
        instance.OnUseConsumableCallback = null;
        instance.OnUseConsumableCallback += stats.OnUseConsumable;
    }
    //使用消耗品 如果不能用返回false
    public bool UseConsumable(Consumable  consumable)
    {
        if (consumable.Type == ConsumablesType.Hp)
        {
            if (stats.currentHealth == stats.maxHealth)
            {
                return false;
            }
        }

        if (UIController.instance.buffImagesDir.ContainsKey(consumable.name))
        {
            return false;
        }
        //使用消耗品的回调函数
        OnUseConsumableCallback?.Invoke(consumable);
        return true;
    }

 
}
