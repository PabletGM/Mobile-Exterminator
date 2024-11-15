using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class DamageVisualizerShake : DamageVisualizer
{
    [SerializeField] Shaker shaker;

    //we override it and make the TookDamage
    protected override void TookDamage(float health, float amount, float maxHealth, GameObject Instigator)
    {
        base.TookDamage(health, amount, maxHealth, Instigator);
        if(shaker!=null)
        {
            shaker.StartShake();
        }
    }

    //Unity calls the Start and Update methods from the parent class unless they are explicitly overridden in the child class
}
    