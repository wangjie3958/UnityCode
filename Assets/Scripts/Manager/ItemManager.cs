using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class ItemManager : MonoBehaviour
{
    public static ItemManager instance;
    public  bool isFirstToGame=false;
    public  bool isFirstToDesert=false;


    //Game场景的装备和物品预制件
    public List<ItemToGameObject> itemToGameObjectsInGame;
    Dictionary<ItemToGameObject, Vector3> dirGame;
    //Desert场景的装备和物品
    public List<ItemToGameObject> itemToGameObjectsInDesert;
    Dictionary<ItemToGameObject,Vector3 > dirDesert;
    public List<ItemToGameObject> AllItemToGameObjects;

    // Start is called before the first frame update
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
        dirGame = new Dictionary<ItemToGameObject, Vector3>();
        dirDesert = new Dictionary<ItemToGameObject, Vector3>();
        //初始化dir集合
        foreach (var item in itemToGameObjectsInGame)
        {
            dirGame.Add(item,item.position);
        }   
        //初始化dir集合
        foreach (var item in itemToGameObjectsInDesert)
        {
            dirDesert.Add(item,item.position);
        }
    }
    void Start()
    {
    }

    // Update is called once per frame
    void Update()
    {
        
    }
    //生成item
    private GameObject GetGoByItem(Item itemPa)
    {
        foreach (var itemToObject in AllItemToGameObjects)
        {
            if (itemPa == itemToObject.item)
            {
                return itemToObject.gameObject;
            }
        }
        return null;
    }
    //player丢掉装备或者boss爆出装备
    public  void CreateDiscordItem(Vector3 position,Item item)
    {
        Vector3 offset = new Vector3(Random.Range(0, 2), 1, Random.Range(0, 2));
        GameObject go =  GetGoByItem(item);
        Instantiate(go, position + offset,Quaternion.identity);
        AddPickUp(item, position + offset);
    }
    //Player捡起装备

    public void Init()
    {
        if (SceneManager.GetActiveScene().name==SceneEnum.Game.ToString())
        {
            //init
            foreach (var item in itemToGameObjectsInGame)
            {
                GameObject go = Instantiate(item.gameObject);
                if (!isFirstToGame)
                {
                    isFirstToGame = true;
                    go.transform.position = item.position;
                }
                else
                {
                    go.transform.position = dirGame[item];
                }
            }
        }
        else
        {
            foreach (var item in itemToGameObjectsInDesert)
            {
                GameObject go = Instantiate(item.gameObject);
                if (!isFirstToDesert)
                {
                    isFirstToDesert = true;
                    go.transform.position = item.position;
                }
                else
                {
                    go.transform.position = dirDesert[item];
                }
            }

        }
    }
    //玩家捡到装备物品移除
    public void RemovePickUp(Item item)
    {
        if (SceneManager.GetActiveScene().name == SceneEnum.Game.ToString())
        {
           itemToGameObjectsInGame.Remove(GetObjectBtItem(item));
        }
        else if (SceneManager.GetActiveScene().name == SceneEnum.FoundSword.ToString())
        {
            itemToGameObjectsInDesert.Remove(GetObjectBtItem(item));
        }

    }
    //玩家丢掉装备增加
    private void AddPickUp(Item item, Vector3 position)
    {
        ItemToGameObject iGo = GetObjectBtItem(item);
        if (SceneManager.GetActiveScene().name == SceneEnum.Game.ToString())
        {
            itemToGameObjectsInGame.Add(iGo);
            dirGame[iGo]=position;
        }else if(SceneManager.GetActiveScene().name == SceneEnum.FoundSword.ToString())
        {
            itemToGameObjectsInDesert.Add(iGo);
            dirDesert[iGo]=position;
        }
        
    }
    private ItemToGameObject GetObjectBtItem(Item item)
    {
        foreach (var itemToGameObject in AllItemToGameObjects)
        {
            if (item == itemToGameObject.item)
            {
                return itemToGameObject;
            }
        }
        return null;
    }


}
