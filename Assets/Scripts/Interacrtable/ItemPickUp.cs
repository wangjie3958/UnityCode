
using System;
using UnityEngine;

public class ItemPickUp : Interactable
{
    public Item item;
    public event Action<Item> OnPickUpCallBack;
    private void Start()
    {
        OnPickUpCallBack += ItemManager.instance.RemovePickUp;
    }
    public override void Interact()
    {
        base.Interact();
        PickUp();
    }
    void PickUp()
    {
        Debug.Log("Try Pick Up:"+item.name);
        //Add a inventory
       bool wasPickUp =  Inventory.instance.Add(item);
        //移除场景管理的对象
        OnPickUpCallBack?.Invoke(item);
        if (wasPickUp)
        Destroy(gameObject);

    }
}
