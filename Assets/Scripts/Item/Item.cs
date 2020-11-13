
using UnityEngine;


public class Item : ScriptableObject
{
    //new关键字是不继承object的name字段
  new  public string name = "new item";
    public Sprite icon = null;
    public bool isDefaultItem = false;
    [TextArea]
    public string introduce;

    public virtual void Use()
    {
        //Use the item 
        //Somethins might happen
       // Debug.Log("Using" + name);
    }
    //使用装备在装备栏删除
    public void RemoveFromInventory()
    {
        Inventory.instance.Remove(this);
    }

}
