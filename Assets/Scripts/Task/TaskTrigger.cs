using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class TaskTrigger : MonoBehaviour
{
    public TaskItem task;
    private void Awake()
    {
        task.isComplete = false;
    }
    //任务触发添加到任务管理器上
    public void AddTask()
    {
        TaskManager.instance.AddTask(task);
    }
}
