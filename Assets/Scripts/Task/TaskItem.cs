using System.Collections;
using System.Collections.Generic;
using UnityEngine;
[CreateAssetMenu(fileName ="task")]
public class TaskItem : ScriptableObject
{
    //task 状态
    public bool isComplete = false;
    //task Name
    public string Name;
    //task introduce
    public string introduce;
}
