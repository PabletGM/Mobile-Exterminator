using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class HealthComponent : MonoBehaviour
{
    public delegate void OnHealthChange(float health, float amount, float maxHealth);
    public delegate void OnTakeDamage(float health, float amount, float maxHealth, GameObject Instigator);
    public delegate void OnHealthEmpty();

    [SerializeField] float health = 100;
    [SerializeField] float maxhealth = 100;

    public event OnHealthChange onHealthChange;
    public event OnTakeDamage onTakeDamage;
    public event OnHealthEmpty onHealthEmpty;

    public void BroadcastHealthValueInmediately()
    {
        //if there is regenerate health or 0 damage
        onHealthChange?.Invoke(health, 0, maxhealth);
    }


    public void changeHealth(float amount, GameObject Instigator)
    {
        //no damage
        if(amount == 0 || health == 0)
        {
            return;
        }

        //-damage or + health regeneration
        health += amount;

        //if there is damage
        if(amount < 0)
        {
            onTakeDamage?.Invoke(health, amount, maxhealth, Instigator);
        }
        //if there is regenerate health or 0 damage
        onHealthChange?.Invoke(health, amount, maxhealth);
        

        //if there is no health
        if(health <= 0)
        {
            onHealthEmpty?.Invoke();
        }

        Debug.Log($"{gameObject.name}, taking damage: {amount}, health is now: {health}");
    }
}
