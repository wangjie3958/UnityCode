
using UnityEngine;

public class DesertSceneManagerInit : MonoBehaviour
{

    public static  DesertSceneManagerInit instance;


    // Start is called before the first frame update
    void Awake()
    {
        if (instance == null)
        {
            instance = this;
        }
        else
        {
            Destroy(gameObject);
        }
        //初始化player关联的所有预制件
        Instantiate(PlayerAll);
    }
    public GameObject PlayerAll;
    private void Start()
    {//初始化BGM
        MainBgmAudioManager.instance.Init();
        //初始化玩家信息
        PlayerManager.instance.Init();
       //初始化背包
        Inventory.instance.Init();
        //初始化玩家装备
        EquiomentManager.instance.Init();
        //初始化消耗品
        ConsumablesManager.instance.Init();
         //初始化可交互的物品
        ItemManager.instance.Init();
        //初始化任务和UI的回调
        TaskManager.instance.Init();


    }
}
