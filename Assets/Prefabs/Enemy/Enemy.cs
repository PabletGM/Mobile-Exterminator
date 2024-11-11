using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public abstract class Enemy : MonoBehaviour, IBehaviourTreeInterface
{
    //reference of the HealthComponent
    [SerializeField] HealthComponent healthComponent;
    //reference to the animator
    [SerializeField] Animator animator;
    //reference to the perception component
    [SerializeField] PerceptionComponent perceptionComponent;

    [SerializeField] BehaviourTree behaviourTree;

    //component
    [SerializeField] MovementComponent movementComponent;


    //get set animator
    public Animator Animator
    {
        get { return animator; }
        private set { animator = value; }
    }
    
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

    //add a parameter
    private void TargetChanged(GameObject target, bool sensed)
    {
       if(sensed)
       {
            //add the info parameter Target
            behaviourTree.Blackboard.SetOrAddData("Target",target);
       }
       else
       {
            //adding an argument of LastSeenLocation
            behaviourTree.Blackboard.SetOrAddData("LastSeenLocation", target.transform.position);
            //remove the info parameter Target
            behaviourTree.Blackboard.RemoveBlackboardData("Target");
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
        //if it exists "Target" on the blackboard
        if(behaviourTree && behaviourTree.Blackboard.GetBlackboardData("Target", out GameObject target))
        {
            // Set the gizmo color to red
            Gizmos.color = Color.red;

            //draw a sphere in the target
            Vector3 drawTargetPos = target.transform.position + Vector3.up;
            Gizmos.DrawWireSphere(drawTargetPos, 0.7f);
            //draw a Line from the enemy to the player
            Gizmos.DrawLine(transform.position + Vector3.up, drawTargetPos);
        }
    }
//==========================================================================================================================
    //rotate to look a target 
    public void RotateTowards(GameObject target, bool verticalAim)
    {
        //calculate the aimDirection excluding vertical differences
        Vector3 aimDir = target.transform.position - transform.position;
        //the value of aimDir.y depends on if verticalAim is false or true and it will be if true 0
        aimDir.y = verticalAim ? aimDir.y : 0;
        aimDir = aimDir.normalized;
        //ask our movement component to rotate
        movementComponent.AimRotation(aimDir);
    }

    public virtual void AttackTarget(GameObject target)
    {
        //override in child
    }
}
