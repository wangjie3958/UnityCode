using System.Collections;
using System.Collections.Generic;
using System.Threading.Tasks;
using UnityEngine;

public class NpcInteractable : Interactable
{
    public Dialogue[]  AllDialogue;
    public Item giveItem;
    TaskTrigger taskTrigger;
    public GameObject TPGo;
    public Transform TPGoTransform;
    //
    public bool firstGiveItem = true;
    private void Start()
    {
        taskTrigger = GetComponent<TaskTrigger>();
    }
    public override void Interact()
    {
        base.Interact();

        Dictionary<string,TaskItem> tasks = TaskManager.instance.tasks;
        if (!tasks.ContainsKey("击杀骷髅怪"))
        {
            //赐予任务开启传送门让玩家去寻找打败骷髅怪的装备
            AllDialogue[0].dialogueOver = () => { taskTrigger.AddTask(); TPGo.SetActive(true); };
            GameScenceDialogueController.instance.SetDialogue(AllDialogue[0]);
        }
        else 
        {
            //击杀了骷髅怪
            if (tasks["击杀骷髅怪"].isComplete)
            {
                //第一次去领任务
                if (firstGiveItem)
                {
                    //给与装备
                    AllDialogue[1].dialogueOver = () => { Inventory.instance.Add(giveItem); UIController.instance.OpenGetItemTips(giveItem); };
                    GameScenceDialogueController.instance.SetDialogue(AllDialogue[1]);
                    firstGiveItem = false;
                }
                else
                {
                    //让玩家滚开
                    GameScenceDialogueController.instance.SetDialogue(AllDialogue[3]);

                }
            }
            else
            {
                //提醒玩家去完成任务
                GameScenceDialogueController.instance.SetDialogue(AllDialogue[2]);
            }
        }

    }
}
