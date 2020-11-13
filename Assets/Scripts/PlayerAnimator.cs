using System.Collections;
using System.Collections.Generic;
using System.Linq;
using UnityEngine;

public class PlayerAnimator : CharacterAnimator
{
    public WeaponAnimations[] weaponAnimations;
    Dictionary<Equipment, AnimationClip[]> weaponAnimationDict;
    protected override void Awake()
    {
        base.Awake();
        weaponAnimationDict = new Dictionary<Equipment, AnimationClip[]>();
        foreach (WeaponAnimations item in weaponAnimations)
        {
            weaponAnimationDict.Add(item.weapon, item.clips);
        }
    }
   public  void OnEquipmentChanged(Equipment newItem,Equipment oldItem)
    {
        if (newItem != null && newItem.equipSlot == EquipmentSlot.Weapon)
        {
            animator.SetLayerWeight(1, 1);
            if (weaponAnimationDict.ContainsKey(newItem))
            {
                currentAttackAnimSet = weaponAnimationDict[newItem];
            }
        }
        else if(newItem==null&&oldItem!=null&&oldItem.equipSlot==EquipmentSlot.Weapon){
            animator.SetLayerWeight(1, 0);
            currentAttackAnimSet = defaultAttackAnimSet;
        } 
        if (newItem != null && newItem.equipSlot == EquipmentSlot.Shield)
        {
            animator.SetLayerWeight(2, 1);
        }
        else if(newItem==null&&oldItem!=null&&oldItem.equipSlot==EquipmentSlot.Shield){
            animator.SetLayerWeight(2, 0);
        }

    }
    //初始化装备的时候调节动画
    public void OnEquipmentChangedInit(Equipment[] equipments)
    {
        for (int i = 0; i < equipments.Length; i++)
        {
            if (equipments[i] != null)
            {
                if (equipments[i].equipSlot == EquipmentSlot.Weapon)
                {
                    animator.SetLayerWeight(1, 1);
                    if (weaponAnimationDict.ContainsKey(equipments[i]))
                    {
                        currentAttackAnimSet = weaponAnimationDict[equipments[i]];
                    }
                }else if (equipments[i].equipSlot == EquipmentSlot.Shield)
                {
                    animator.SetLayerWeight(2, 1);
                }

            }

        }
    }
    [System.Serializable]
    public struct WeaponAnimations
    {
        public Equipment weapon;
        public AnimationClip[] clips;
    }
}
