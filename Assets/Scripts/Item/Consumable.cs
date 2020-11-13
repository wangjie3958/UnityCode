using System.Collections;
using System.Collections.Generic;
using UnityEngine;
[CreateAssetMenu(fileName = "New Consumables", menuName = "Inventory/Consumables")]
public class Consumable : Item
{
    //增加的数值
    public int IncreaseNumber = 10;
    //消耗品类型
    public ConsumablesType Type;
    //持续时间
    public int KeepTime = 10;
    //buffImage
    public Sprite buffSprite;
    public override void Use()
    {
        base.Use();
        //使用消耗品
        if(ConsumablesManager.instance.UseConsumable(this))
        RemoveFromInventory();
    }

}
//消耗品类型
public enum ConsumablesType
{
    Hp,
    Damage,
    Armour
}