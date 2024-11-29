using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public abstract class Enemy : MonoBehaviour, IBehaviourTreeInterface, ITeamInterface, ISpawnInterface
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

    //default value of enemy team
    [SerializeField] int TeamID = 2;

     Vector3 previousPosition;

    public int GetTeamID()
    {
        return TeamID;
    }


    //get set animator
    public Animator Animator
    {
        get { return animator; }
        private set { animator = value; }
    }

    private void Awake()
    {
        //we suscribe a method to the event onPerceptionTargetChanged
        perceptionComponent.onPerceptionTargetChanged += TargetChanged;
    }

    protected virtual void Start()
    {
        //if there is healthComp
        if(healthComponent != null)
        {
            //we suscribe the method startDeath to event OnHealthEmpty
            healthComponent.onHealthEmpty += StartDeath;
            //we suscribe the method TakenDamage to event OnTakeDamage
            healthComponent.onTakeDamage += TakenDamage;
        }

        
        previousPosition = transform.position;
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
        CalculateSpeed();

    }

    private void CalculateSpeed()
    {
        if (movementComponent == null) return;
        
        //check how much has moved from initial position
        Vector3 posDelta = transform.position - previousPosition;
        //calculate the speed = space / time
        float speed = posDelta.magnitude / Time.deltaTime;

        Animator.SetFloat("Speed", speed);

        //we restart the previousPosition
        previousPosition = transform.position;
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

    //we want to make here when a enemy is spawned all the behaviour
    public void SpawnedBy(GameObject spawnerGameObject)
    {
        //get the target of the spawner to give it to the childs they spawn so they share Target

        //take behaviour tree of spawner
        BehaviourTree spawnerBehaviourTree = spawnerGameObject.GetComponent<BehaviourTree>();
        //take the target of the spawner(the player)
        if(spawnerBehaviourTree != null && spawnerBehaviourTree.Blackboard.GetBlackboardData<GameObject>("Target", out GameObject spawnerTarget))
        {
            //the player has a stimuli, so if he has it, he can be targeted, and the enemy has a perceptionComponent to perceive the stimuli?
            PerceptionStimuli  targetStimuli = spawnerTarget.GetComponent<PerceptionStimuli>();
            if(perceptionComponent && targetStimuli)
            {
                //assign enemy perception component a new stimuli of the player
                perceptionComponent.AssignPerceivedStimuli(targetStimuli);
            }
        }
    }
}
