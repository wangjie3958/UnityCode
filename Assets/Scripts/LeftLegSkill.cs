using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class LeftLegSkill : MonoBehaviour
{
   public  PlayerAudioController playerAudioController;
    public int Damage;
    
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
        if(other.CompareTag("Enemy")&& playerAudioController.isStartSkill)
        {
            if(!other.gameObject.GetComponent<EnmetStats>().isDead)
            other.transform.GetComponent<CharacterStats>().TakeDamage(10);
        }
    }
}
