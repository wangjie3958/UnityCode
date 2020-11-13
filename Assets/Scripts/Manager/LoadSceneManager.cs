using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;
using UnityEngine.UI;

public class LoadSceneManager : MonoBehaviour
{
    public static string SceneName;
    public static LoadSceneManager instance;
    //获取Slider
    public  Slider slider;
    //获取显示TEXT
    public  Text loadScenePercent;
    private void Awake()
    {       
        instance = this;
    }
    private void Start()
    {
       StartCoroutine( loadSceneAsyc(SceneName));
    }

    IEnumerator loadSceneAsyc(string sceneName)
    {
        AsyncOperation asyncOperation =  SceneManager.LoadSceneAsync(sceneName);
        asyncOperation.allowSceneActivation = false;
        while (!asyncOperation.isDone)
        {
            loadScenePercent.text = (asyncOperation.progress*100).ToString()+"%";
            slider.value = asyncOperation.progress;
            if (asyncOperation.progress >= 0.9f)
            {
                loadScenePercent.text = "按下任意键继续";
                slider.value = 1;
                if (Input.anyKeyDown)
                {
                    asyncOperation.allowSceneActivation = true;
                }
            }
            yield return null;
        }
    }


}
public enum SceneEnum
{
    Game,
    Menu,
    Load,
    FoundSword
}