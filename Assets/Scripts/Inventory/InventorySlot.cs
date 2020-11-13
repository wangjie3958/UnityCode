using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class InventorySlot : MonoBehaviour
{
    public Image icon;
    public Button removeButton;
    public Transform playTransformt;
    //信息介绍框
    public RectTransform HoveredInfo;
    public Text IntroduceText;
    Item item;

    public void AddItem(Item newItem)
    {
        item = newItem;
        icon.sprite = item.icon;
        icon.enabled = true;
        removeButton.interactable = true;
    }
    public void ClearSlot()
    {
        item = null;
        icon.sprite = null;
        icon.enabled = false;
        removeButton.interactable = false; 

    }
    public void OnRemoveButton()
    {
        //生成在玩家附近
        ItemManager.instance.CreateDiscordItem(playTransformt.position, item);
        //移除背包的Item
        Inventory.instance.Remove(item);


    }

    public void UseItem()
    {
        if (item != null)
        {
            item.Use();
        }

    }
    //鼠标移入移动介绍信息框在合适的位置
    public void OnPointEnter()
    {
        if (item != null)
        {
            HoveredInfo.gameObject.SetActive(true);
            IntroduceText.text = item.introduce;
        }
        else
        {
            HoveredInfo.gameObject.SetActive(false);
        }
    }

}
