using System.Collections;
using System.Collections.Generic;
using TMPro;
using UnityEngine;
using UnityEngine.SceneManagement;
using UnityEngine.UI;

public class UIController : MonoBehaviour
{
    public static UIController instance;
    public Image QImage;
    public Image WImage;
    public Image EImage;

    //任务UI的父节点
    public GameObject taskParent;
    //任务prefabsUI
    public GameObject taskeUI;
    public Sprite notCompletedSprite;
    public Sprite completedSprite;
   public  Dictionary<string, TaskItem> tasks;
    //消耗品的buffUi
    public Transform BuffTarget;
    //消耗品预制件
    public GameObject BuffUI;
   public  PlayerStats Stats;
    //buff管理集合
   public Dictionary<string, GameObject> buffImagesDir;
    //用户状态栏信息
    [Header("State")]
    public Image HpImage;
    public Text HpText;
    public Text PowerText;
    public Text ArmourText;
    private void Awake()
    {
        instance = this;
        buffImagesDir = new Dictionary<string, GameObject>();
    }
    public PlayerSkillController playerSkillController;
    private void Start()
    {
        //给技能改变添加监听事件
        playerSkillController.OnSkillDelayChanged += SetSkillDelayIcon;
        //添加任务改变事件
        tasks = TaskManager.instance.tasks;
        Stats.BuffPercent += OnUseConsumable;
        //显示地图名字
        if (SceneManager.GetActiveScene().name == SceneEnum.Game.ToString())
        {
            ViewMapName("骑士城");
        }
        else
        {
            ViewMapName("沙漠城");
        }
        
        //新场景初始化任务UI
        OnTasksChanged();
         }
    private void Update()
    {
        //设置用户状态栏
        HpImage.fillAmount = (float)Stats.currentHealth / Stats.maxHealth;
        HpText.text = Stats.currentHealth + "/" + Stats.maxHealth;
        PowerText.text = Stats.damage.GetValue().ToString();
        ArmourText.text = Stats.armor.GetValue().ToString();
    }
    //设置技能cd
    public void SetSkillDelayIcon(float QDelayPercent,float WDelayPercent, float EDelayPercent)
    {
        QImage.fillAmount = QDelayPercent;
        WImage.fillAmount = WDelayPercent;
        EImage.fillAmount = EDelayPercent;
    }
    public GameObject TasksPanel;
    //任务页面开启关闭
    public void OpenCloseTaskPanel()
    {
        TasksPanel.SetActive(!TasksPanel.activeSelf);
    }

 
    //UI任务显示
    public void OnTasksChanged()
    {
        //删除所有子任务对象
        foreach (Transform item in taskParent.GetComponentInChildren<Transform>())
        {
            Destroy(item.gameObject);
        } 

        foreach (string taskName in tasks.Keys)
        {
            GameObject go = Instantiate(taskeUI, taskParent.transform);
            Text[] texts = go.GetComponentsInChildren<Text>();
            foreach (Text item in texts)
            {
                if (item.CompareTag("taskName"))
                {
                    item.text = tasks[taskName].Name;
                }
                if (item.CompareTag("taskIntroduce"))
                {
                    item.text = tasks[taskName].introduce;
                }
            }
            Image[] images = go.GetComponentsInChildren<Image>();
            foreach (Image item in images)
            {
                if (item.CompareTag("taskCompleted"))
                {
                    if (tasks[taskName].isComplete)
                    {
                        images[1].sprite = completedSprite;
                    }
                    else
                    {
                        images[1].sprite = notCompletedSprite;
                    }

                }
            }

        }
    }

    //UI任务button闪烁
    public GameObject UITaskButtonGlow;
    public void GlowTaskUI()
    {
        StartCoroutine(Twinkle(UITaskButtonGlow,5,0.5f));
    }
    //闪烁协程
    IEnumerator Twinkle(GameObject gameObject,int twinkleTime, float twinkleIntervalTime)
    {
        for (int i = 0; i < twinkleTime; i++)
        {
            gameObject.SetActive(!gameObject.activeSelf);
            yield return new WaitForSeconds(twinkleIntervalTime);
            if (i >= twinkleTime - 1)
            {
                gameObject.SetActive(false);
            }
        }
        
    }

    //UIbuff显示
    public void OnUseConsumable(int currentUseTime,Consumable consumable)
    {
        if (currentUseTime == 0)
        {
            GameObject buffUI = Instantiate(BuffUI, BuffTarget);
            buffUI.GetComponentsInChildren<Image>()[0].sprite = consumable.buffSprite;
            Image uiImage = buffUI.GetComponentsInChildren<Image>()[1];
            uiImage.sprite = consumable.buffSprite;
            buffImagesDir.Add(consumable.name, buffUI);
            uiImage.fillAmount = (currentUseTime+1) / consumable.KeepTime;
        }
        else
        {
            buffImagesDir[consumable.name].GetComponentsInChildren<Image>()[1].fillAmount = (float)(currentUseTime + 1) / consumable.KeepTime;
            if (currentUseTime+1 >= consumable.KeepTime)
            {
                //删除该buff
                Destroy(buffImagesDir[consumable.name]);
                buffImagesDir.Remove(consumable.name);
            }
        }
     }

    //音量调节panel
    public GameObject modulationPanel;

    //音量调节 button
    public void OpenCloseSoundRegulation()
    {
            modulationPanel.SetActive(!modulationPanel.activeSelf);
    }

    public GameObject PlayerStateUI;
    //开启和关闭任务状态栏
    public void OpenClosePlayerState()
    {
        PlayerStateUI.SetActive(!PlayerStateUI.activeSelf);
    }
    //开启获得装备UI提示
    public GameObject ItemTipsUI;
    public Text TipsText;
    public void OpenGetItemTips(Item item)
    {
        ItemTipsUI.SetActive(true);
        TipsText.text = "你获得了" + item.name;
        //播放音效
        SoundEffectManager.instance.PlaySoundEffect(0);
    }

    //地图名字UI显示
    public GameObject MapNameObject;
    public TextMeshProUGUI MapNameText;
    public void ViewMapName(string mapName)
    {
        MapNameObject.SetActive(true);
        MapNameText.text = mapName;
        StartCoroutine(ShowUITime(MapNameObject, 1.5f));
    }
    //显示一个UI一定时间
    IEnumerator ShowUITime(GameObject go,float time)
    {
        float i = 0f;
        while (i <= time)
        {
            i += 0.1f;
            yield return new WaitForSeconds(0.1f);
        }
        go.SetActive(false);
    }
}
