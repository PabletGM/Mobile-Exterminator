using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Enemy : MonoBehaviour
{
    //reference of the HealthComponent
    [SerializeField] HealthComponent healthComponent;
    //reference to the animator
    [SerializeField] Animator animator;
    void Start()
    {
        //if there is healthComp
        if(healthComponent != null)
        {
            //we suscribe the method startDeath to event OnHealthEmpty
            healthComponent.onHealthEmpty += StartDeath;
            //we suscribe the method TakenDamage to event OnTakeDamage
            healthComponent.onTakeDamage += TakenDamage;
        }
    }

    private void TakenDamage(float health, float amount, float maxHealth)
    {
        
    }



//================================================================================================================================================
    
    private void StartDeath()
    {
        TriggerDeathAnimation();
    }

    private void TriggerDeathAnimation()
    {
        //if exists animator
        if (animator != null)
        {
            //trigger death animation
            animator.SetTrigger("Dead");
        }
    }

    public void OnDeathAnimationFinished()
    {
        Destroy(gameObject);
    }

//================================================================================================================================================

    // Update is called once per frame
    void Update()
    {
        
    }
}
