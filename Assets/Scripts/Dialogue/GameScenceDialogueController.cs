using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class GameScenceDialogueController : MonoBehaviour
{
    public static GameScenceDialogueController instance;

    public GameObject dialogUI;
    public Image headImage;
    public Text NameText;
    public Text SentencesText;
    Dialogue dialogueCurrent;
    //对话完的事件调用
    private Queue<string> sentences;
    private void Awake()
    {
        instance = this;
        sentences = new Queue<string>();
    }
    public void SetDialogue(Dialogue dialogue)
    {
        dialogueCurrent = dialogue;
        dialogUI.SetActive(true);
        headImage.sprite = dialogue.spriteIcon;
        NameText.text = dialogue.Name;
        foreach (string sentence in dialogue.Sentences)
        {
            sentences.Enqueue(sentence);
        }
        NextSentenceSet();
    }
    public  void NextSentenceSet()
    {
        if (sentences.Count <= 0)
        {
            EndSentence();
            //对话结束的事件只调用一次
            dialogueCurrent.dialogueOver?.Invoke();
            
        }
        else
        {
            SentencesText.text = null;
            StartCoroutine(SetSentenceByWords(sentences.Dequeue())) ;
        }
    }
    //一个一个字显示
    IEnumerator SetSentenceByWords(string sentence)
    {
        foreach (char word in sentence.ToCharArray())
        {
            SentencesText.text += word;
            yield return null;
        }

    }
    private void EndSentence()
    {
        dialogUI.SetActive(false);
    }
}
