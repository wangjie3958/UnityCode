using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class BeamTrigger : MonoBehaviour
{
    public SceneEnum sceneEnum;
    // Start is called before the first frame update
    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {
        
    }
    private void OnTriggerEnter(Collider other)
    {
        if (other.transform.CompareTag("Player"))
        {
            //场景迁移
            LoadSceneManager.SceneName = sceneEnum.ToString();
            SceneManager.LoadScene(SceneEnum.Load.ToString());
            //将player的血量
            PlayerStats stats = other.GetComponent<PlayerStats>();
            PlayerManager.instance.SavePlayerStats(stats.currentHealth,stats.armor.getModifiers(),stats.damage.getModifiers());
        }
    }
}
