using System.Collections;
using System.Collections.Generic;
using UnityEngine;
[RequireComponent(typeof(CharacterStats))]
public class Enemy : Interactable
{
    CharacterStats myStats;
    GameObject player;
    private void Start()
    {
        player = FindObjectOfType<PlayerStats>().gameObject;
        myStats = GetComponent<CharacterStats>();
    }
    public override void Interact()
    {
        base.Interact();
        //Attack the Enemy
       CharacterCombat playerCombat = player.GetComponent<CharacterCombat>();
        if (playerCombat != null)
        {
            playerCombat.Attack(myStats);
        }
    }
}
