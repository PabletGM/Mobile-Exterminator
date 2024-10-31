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
    //reference to the perception component
    [SerializeField] PerceptionComponent perceptionComponent;

    GameObject Target;
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

        //we suscribe a method to the event onPerceptionTargetChanged
        perceptionComponent.onPerceptionTargetChanged += TargetChanged;
    }

    private void TargetChanged(GameObject target, bool sensed)
    {
       if(sensed)
       {
            this.Target = target;
       }
       else
       {
            this.Target = null; 
       }
    }

    private void TakenDamage(float health, float amount, float maxHealth, GameObject Instigator)
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

    private void OnDrawGizmos()
    {
        if(Target != null)
        {
            // Set the gizmo color to red
            Gizmos.color = Color.red;

            //draw a sphere in the target
            Vector3 drawTargetPos = Target.transform.position + Vector3.up;
            Gizmos.DrawWireSphere(drawTargetPos, 0.7f);
            //draw a Line from the enemy to the player
            Gizmos.DrawLine(transform.position + Vector3.up, drawTargetPos);
        }
    }
}
