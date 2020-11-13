using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class PlayerManager : MonoBehaviour
{
    public Vector3 PlayerPositionInGame;
    public Vector3 PlayerPositionInDesert;
    Vector3 PlayerPosition;
    int hp=100;
    List<int> armorModifiers;
    List<int> damageModifiers;
    #region
    public static PlayerManager instance;
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
        armorModifiers = new List<int>();
        damageModifiers = new List<int>();
    }
    #endregion

    public void Init()
    {
        if (SceneManager.GetActiveScene().name == SceneEnum.Game.ToString())
        {
            PlayerPosition = PlayerPositionInGame;
        }else if(SceneManager.GetActiveScene().name == SceneEnum.FoundSword.ToString())
        {
            PlayerPosition = PlayerPositionInDesert;
        }
        //初始化玩家位置
        PlayerStats playerStats = FindObjectOfType<PlayerStats>();
        playerStats.transform.position = PlayerPosition;
        //初始化玩家的状态 血量 伤害 防御
        playerStats.currentHealth=hp;
        playerStats.armor.setModifiers(armorModifiers);
        playerStats.damage.setModifiers(damageModifiers);
    }

    //保存人物状态信息
    public  void SavePlayerStats(int hp,List<int> armorModifiers, List<int> damageModifiers)
    {
        this.hp = hp;
        this.armorModifiers = armorModifiers;
        this.damageModifiers = damageModifiers;
    }
    public void KillPlayer()
    {
        SceneManager.LoadScene(SceneManager.GetActiveScene().buildIndex);
    }
}
