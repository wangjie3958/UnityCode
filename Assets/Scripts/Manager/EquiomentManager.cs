using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class EquiomentManager : MonoBehaviour
{
    #region Singleton
    public static EquiomentManager instance;
    private void Awake()
    {
        if (instance == null)
        {
            instance = this;
        }
        else
        {
            Destroy(gameObject);
        }
    }
    #endregion
    public Equipment[] defalutItems;
    public delegate void OnEquipmentChanged(Equipment newItem, Equipment oldItem);
    public OnEquipmentChanged onEquipmentChanged;

    public SkinnedMeshRenderer targetMesh;
     Transform leftHand;
     Transform rightHand;
    public Equipment[] currentEquipment;
    SkinnedMeshRenderer[] currentMeshes;
    Inventory inventory;
    public void Init()
    {
        //初始化玩家装备玩家的状态回调
        PlayerStats stats = GameObject.FindGameObjectWithTag("Player").GetComponent<PlayerStats>();
        instance.onEquipmentChanged = null;
        instance.onEquipmentChanged += stats.OnEquipmentChanged;
        //初始化玩家在UI更换装备inventory显示的回调
        InventoryUI inventoryUI = FindObjectOfType<InventoryUI>().GetComponent<InventoryUI>();
        instance.onEquipmentChanged += inventoryUI.UpdateRoleEquipment;
        //初始化玩家更换装备攻击声音的回调
        PlayerAudioController audioController = FindObjectOfType<PlayerAudioController>();
        instance.onEquipmentChanged += audioController.OnEquipmentChanged;
        //初始化更换装备后玩家攻击动画的回调
        PlayerAnimator playerAnimator = FindObjectOfType<PlayerAnimator>();
        instance.onEquipmentChanged += playerAnimator.OnEquipmentChanged;
        //初始化左右手的骨骼
        leftHand = GameObject.FindGameObjectWithTag("Shield").transform;
        rightHand = GameObject.FindGameObjectWithTag("Sword").transform;
        //初始化player的mesh用以调节衣服穿模
        targetMesh = GameObject.FindGameObjectWithTag("Body").GetComponent<SkinnedMeshRenderer>();

        inventory = Inventory.instance;
        int numSlots = Enum.GetNames(typeof(EquipmentSlot)).Length;
        currentMeshes = new SkinnedMeshRenderer[numSlots];
        //第一次装备默认的Mesh
        if (currentEquipment == null || currentEquipment.Length < numSlots)
        {
            currentEquipment = new Equipment[numSlots];
            EquipDefaultItems();
        }
        else
        {  //初始化玩家装备Mesh
            EquipInit();
            //初始化各种装备不同的动画
            playerAnimator.OnEquipmentChangedInit(currentEquipment);
            //初始化各种装备不同的声音
            audioController.OnEquipmentInit(currentEquipment);
        }
    }

    //在插槽中使用装备
    public void Equip(Equipment newItem)
    {
        if (newItem != null)
        {
            //获取是哪个装备索引
            int slotIndex = (int)newItem.equipSlot;
            //卸下当前装备
            Equipment oldItem = Unequip(slotIndex); ;
            //更换装备回调
            if (onEquipmentChanged != null)
            {
                onEquipmentChanged.Invoke(newItem, oldItem);
            }
            SetEquipmentBlendShapes(newItem, 100);
            currentEquipment[slotIndex] = newItem;
            //newItem.mesh是预制件不能直接使用需要克隆
            SkinnedMeshRenderer newMesh = Instantiate<SkinnedMeshRenderer>(newItem.mesh);
            //Weapon and Shied bones is other
            if (newItem.equipSlot == EquipmentSlot.Weapon)
            {
                newMesh.rootBone = rightHand;
                newMesh.transform.SetParent(rightHand);
            }
            else if (newItem.equipSlot == EquipmentSlot.Shield)
            {
                newMesh.rootBone = leftHand;
                newMesh.transform.SetParent(leftHand);
            }
            else
            {
                newMesh.transform.parent = targetMesh.transform;
                newMesh.bones = targetMesh.bones;
                newMesh.rootBone = targetMesh.rootBone;
            }
            currentMeshes[slotIndex] = newMesh;
        }
    }  
    
    //初始化装备栏
    public void EquipInit()
    {
        for (int i = 0; i < currentEquipment.Length; i++)
        {
            if (currentEquipment[i] != null)
            {

                SetEquipmentBlendShapes(currentEquipment[i], 100);
                //newItem.mesh是预制件不能直接使用需要克隆
                SkinnedMeshRenderer newMesh = Instantiate(currentEquipment[i].mesh);
                //Weapon and Shied bones is other
                if (currentEquipment[i].equipSlot == EquipmentSlot.Weapon)
                {
                    newMesh.rootBone = rightHand;
                    newMesh.transform.SetParent(rightHand);
                }
                else if (currentEquipment[i].equipSlot == EquipmentSlot.Shield)
                {
                    newMesh.rootBone = leftHand;
                    newMesh.transform.SetParent(leftHand);
                }
                else
                {
                    newMesh.transform.parent = targetMesh.transform;
                    newMesh.bones = targetMesh.bones;
                    newMesh.rootBone = targetMesh.rootBone;
                }
                currentMeshes[i] = newMesh;
            }
        }
       
    } 
    //卸下装备 返回卸下的装备
    public Equipment Unequip(int slotIndex)
    {
        if (currentEquipment[slotIndex] != null)
        {

            Equipment oldItem = currentEquipment[slotIndex];

                if (currentMeshes[slotIndex] != null)
            {
                Destroy(currentMeshes[slotIndex].gameObject);
            }
            //SetEquipmentBlendShapes(oldItem, 0);
            inventory.Add(oldItem);
            currentEquipment[slotIndex] = null;
            //更换装备回调
            if (onEquipmentChanged != null)
            {
                onEquipmentChanged.Invoke(null, oldItem);
            }

                return oldItem;
        }

        return null;
    }
    //卸下非默认装备
    public Equipment UnEquipDontDefalut(int slotIndex)
    {
        if (currentEquipment[slotIndex] != null)
        {

            Equipment oldItem = currentEquipment[slotIndex];
            //如果卸下的不是默认装备，才卸下装备否则不卸下
            if (!oldItem.isDefaultItem)
            {
                if (currentMeshes[slotIndex] != null)
            {
                Destroy(currentMeshes[slotIndex].gameObject);
            }
            //SetEquipmentBlendShapes(oldItem, 0);
            inventory.Add(oldItem);
            currentEquipment[slotIndex] = null;
            //更换装备回调
            if (onEquipmentChanged != null)
            {
                onEquipmentChanged.Invoke(null, oldItem);
            }

                switch (oldItem.equipSlot)
                {
                    case EquipmentSlot.Head:
                        Equip(defalutItems[3]);
                        break;
                    case EquipmentSlot.Chest:
                        Equip(defalutItems[1]);
                        break;
                    case EquipmentSlot.Legs:
                        Equip(defalutItems[0]);
                        break;
                    case EquipmentSlot.Weapon:
                        break;
                    case EquipmentSlot.Shield:
                        break;
                    case EquipmentSlot.Feet:
                        break;
                    default:
                        break;
                }
               
            }
            return oldItem;
        }

        return null;
    }
    //卸下所有装备
    public void UnequipAll()
    {
        for (int i = 0; i < currentEquipment.Length; i++)
        {
            Unequip(i);

        }
        //不卸下默认装备
        EquipDefaultItems();
    }
    //设置mesh权重让衣服能全部显示出来
    void SetEquipmentBlendShapes(Equipment item,int weight)
    {
        foreach (EquipmentMeshRegion blendShapes in item.coveredMeshRegions)
        {
            targetMesh.SetBlendShapeWeight((int)blendShapes, weight);
        }
    }
    //装备默认mesh的装备
  public void EquipDefaultItems()
    {
        foreach (Equipment item in defalutItems)
        {
            Equip(item);
        }
    }


    private void Update()
    {
        //按下u键卸下所有装备
        if (Input.GetKeyDown(KeyCode.U))
        {
            UnequipAll();
        }
    }
}
