
using System;
using UnityEngine;

public class CharacterStats : MonoBehaviour
{
    public int maxHealth = 100;
    public int currentHealth;
    public Stat damage;
    public Stat armor;
    public event Action<int, int> OnHealthChanged;
    public event Action<int> OnTakeDamage;
    public bool isDead = false;
    private void Awake()
    {
        currentHealth = maxHealth;
    }

    public void TakeDamage(int damage)
    {
        damage -= armor.GetValue();
        damage = Mathf.Clamp(damage, 0, int.MaxValue);
          currentHealth -= damage;
        //Debug.Log(transform.name + "takes " + damage+ "damage:");
        if (OnHealthChanged != null)
        {
            OnHealthChanged.Invoke(maxHealth, currentHealth);
        }
        OnTakeDamage?.Invoke(damage);
        if (currentHealth <= 0)
        {
            Die();
        }
    }

    public virtual void Die()
    {
        isDead = true;
    }
}
