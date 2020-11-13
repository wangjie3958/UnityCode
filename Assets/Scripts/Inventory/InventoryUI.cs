using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class InventoryUI : MonoBehaviour
{
    public GameObject inventoryUI;
   public  InventorySlot[] slots;

    // Use is for initialization
    void Start()
    {
        //初始化UI库存
        UpdateUI();
        //初始化UI装备栏信息
        InitPlayerStateEquipment();
    }
     void Update()
    {
        if (Input.GetButtonDown("Inventory"))
        {
            inventoryUI.SetActive(!inventoryUI.activeSelf);
        }
        
    }

  public   void UpdateUI()
    {
        for (int i = 0; i < slots.Length; i++)
        {
            if (i < Inventory.instance.items.Count)
            {
                slots[i].AddItem(Inventory.instance.items[i]);
            }
            else
            {
                slots[i].ClearSlot();
            }

        }

    }
    //角色的五个装备栏
    public Image Head;
    public Image Chest;
    public Image Legs;
    public Image Weapon;
    public Image Shield;
    public Sprite UIMask;


    //初始化装备栏
    public void InitRoleEquipment()
    {
        foreach (var item in EquiomentManager.instance.currentEquipment)
        {

        } 
    }

    //管理角色信息的装备栏
   public  void UpdateRoleEquipment(Equipment newItem,Equipment oldItem)
    {
        //上装备
        if (newItem != null&&!newItem.isDefaultItem)
        {
            switch (newItem.equipSlot)
            {
                case EquipmentSlot.Head:
                    Head.sprite = newItem.icon;
                    break;
                case EquipmentSlot.Chest:
                    Chest.sprite = newItem.icon;
                    break;
                case EquipmentSlot.Legs:
                    Legs.sprite = newItem.icon;
                    break;
                case EquipmentSlot.Weapon:
                    Weapon.sprite = newItem.icon;
                    break;
                case EquipmentSlot.Shield:
                    Shield.sprite = newItem.icon;
                    break;
                default:
                    break;
            }

        }
        if (newItem == null  && !oldItem.isDefaultItem)
        {
            //下装备
            switch (oldItem.equipSlot)
            {
                case EquipmentSlot.Head:
                    Head.sprite = UIMask;
                    break;
                case EquipmentSlot.Chest:
                    Chest.sprite = UIMask;
                    break;
                case EquipmentSlot.Legs:
                    Legs.sprite = UIMask;
                    break;
                case EquipmentSlot.Weapon:
                    Weapon.sprite = UIMask;
                    break;
                case EquipmentSlot.Shield:
                    Shield.sprite = UIMask;
                    break;
                default:
                    break;
            }
        }
    }
    //初始化玩家信息装备栏
    public void InitPlayerStateEquipment()
    {
        foreach (var item in EquiomentManager.instance.currentEquipment)
        {
            if (item != null && !item.isDefaultItem)
            {
                switch (item.equipSlot)
                {
                    case EquipmentSlot.Head:
                        Head.sprite = item.icon;
                        break;
                    case EquipmentSlot.Chest:
                        Chest.sprite = item.icon;
                        break;
                    case EquipmentSlot.Legs:
                        Legs.sprite = item.icon;
                        break;
                    case EquipmentSlot.Weapon:
                        Weapon.sprite = item.icon;
                        break;
                    case EquipmentSlot.Shield:
                        Shield.sprite = item.icon;
                        break;
                    default:
                        break;
                }

            }
        }
    }
    //点击装备栏卸下装备
    public void UnEquipDontDefalut(int slotIndex)
    {
        EquiomentManager.instance.UnEquipDontDefalut(slotIndex);
    }
}
