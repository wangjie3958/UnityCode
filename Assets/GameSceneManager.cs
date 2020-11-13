using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class GameSceneManager : MonoBehaviour
{
    public static GameSceneManager instance;
    public EnemyController enemyController;
    public GameObject PlayerAll;

    // Start is called before the first frame update
    private void Awake()
    {
        if (instance == null)
        {
            instance = this;
        }
        else
        {
            Destroy(this.gameObject);
        }
        Instantiate(PlayerAll);

    } 
    void Start()
    {
        //初始化bgm和创建的关系
        MainBgmAudioManager.instance.Init();
        //初始化敌人的目标
        enemyController.target = FindObjectOfType<PlayerStats>().transform;
        //玩家信息初始化
        PlayerManager.instance.Init();
        //初始化库存和UI的绑定
        Inventory.instance.Init();
        //初始化玩家的装备
        EquiomentManager.instance.Init();
        //初始化消耗品和UI的绑定
        ConsumablesManager.instance.Init();
        //初始化Game场景的PickUp
        ItemManager.instance.Init();
        //初始化任务状态
        TaskManager.instance.Init();
    }
}
