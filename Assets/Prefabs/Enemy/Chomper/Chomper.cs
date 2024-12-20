using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Chomper : Enemy
{
    [SerializeField] VFXSpecification[] DeathVFX;
    [SerializeField] TriggerDamageComponent triggerDamageComp;
    //override of the interface  IBehaviourTree method
    public override void AttackTarget(GameObject target)
    {
        Animator.SetTrigger("Attack");
    }

    //animation event on chomper attack melee
    public void AttackPoint()
    {
        //give permission to start the attack
       if(triggerDamageComp)
        {
            triggerDamageComp.SetDamageEnabled(true);
        }
    }

    public void AttackEnd()
    {
        //give permission to start the attack
        if (triggerDamageComp)
        {
            triggerDamageComp.SetDamageEnabled(false);
        }
    }

    protected override void Start()
    {
        base.Start();
        //to initialize the team interface of the DamageComponent
        //triggerDamageComponent and DamageComponent are parent and son so we can call the methods of DamageComp
        triggerDamageComp.SetTeamInterfaceSource(this);
    }

    
    protected override void Dead()
    {
        //foreach VFX on the array instantiate it and 
        //put it on the same position as spawner
        //size
        foreach (VFXSpecification spec in DeathVFX)
        {
            ParticleSystem particleSystem = Instantiate(spec.particleSystem);
            particleSystem.transform.position = transform.position;
            particleSystem.transform.localScale = Vector3.one * spec.size;
        }
    }
}
