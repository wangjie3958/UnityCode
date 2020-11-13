using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class TaskManager : MonoBehaviour
{
    public  static TaskManager instance;
    //任务集合
    public Dictionary<string,TaskItem> tasks;
    //任务改变事件
    public event Action OnTasksChangedCallback;
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

        tasks = new Dictionary<string, TaskItem>();
    }
    public void Init()
    {
      UIController uIController=  FindObjectOfType<UIController>();
        //添加任务改变事件
        instance.OnTasksChangedCallback = null;
        instance.OnTasksChangedCallback += uIController.OnTasksChanged;
        instance.OnTasksChangedCallback += uIController.GlowTaskUI;

    }
    //添加任务
    public void AddTask(TaskItem task)
    {

            tasks.Add(task.Name, task);
        OnTasksChangedCallback?.Invoke();       
    }
    //完成任务
    public void CompletedTask(string name)
    {
        if (IsExistTask(name))
        {
            tasks[name].isComplete = true;
            OnTasksChangedCallback?.Invoke();
        }
    }
    //判断是否已经有任务了
    public bool IsExistTask(string name )
    {
        return tasks.ContainsKey(name);
    }
    //判断任务是否完成
    public bool IsCompletedTask(string name)
    {
        return tasks[name].isComplete;
    }

    //

}
