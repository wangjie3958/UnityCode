using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
[System.Serializable]
[CreateAssetMenu(menuName ="Dialogue",fileName = "Dialogue")]
public class Dialogue:ScriptableObject
{
    //头像
    public Sprite spriteIcon;
    //名字
    public string Name;
    //话
    [TextArea(3,9)]
    public string[] Sentences;
    //对话完后发生的事件
    public   delegate void DialogueOver();
    public DialogueOver dialogueOver;
}
