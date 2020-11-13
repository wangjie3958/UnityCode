using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class menuUIManager : MonoBehaviour
{
    //音量调节panel
    public GameObject modulationPanel;
    public UIAudioController UIAudioController;

    //音量调节 button
    public void OpenCloseSoundRegulation()
    {
        if (modulationPanel.activeSelf)
        {
            modulationPanel.SetActive(false);
        }
        else
        {
            modulationPanel.SetActive(true);
        }
    }


    public void ClickGameButton()
    {
        //按钮音效
        UIAudioController.PlayAudio(0);
        //场景迁移
        LoadSceneManager.SceneName = SceneEnum.Game.ToString();
        SceneManager.LoadScene(SceneEnum.Load.ToString());
    }

}
