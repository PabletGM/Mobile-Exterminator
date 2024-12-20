using System.Collections;
using System.Collections.Generic;
using UnityEngine;

//class that tells you what type of damage can receive and depending of that and the relation tells if should damage or not
public abstract class DamageComponent : MonoBehaviour
{
    [SerializeField] protected bool DamageFriendly;
    [SerializeField] protected bool DamageEnemy;
    [SerializeField] protected bool DamageNeutral;

    ITeamInterface teamInterface;

   
    public void SetTeamInterfaceSource(ITeamInterface teamInterface)
    {
        this.teamInterface = teamInterface;
    }
    
    
    public bool ShouldDamage(GameObject other)
    {

        if(teamInterface == null)
        {
            return false;
        }

        ETeamRelation relation = teamInterface.GetRelationTowards(other);

        if(DamageFriendly && relation == ETeamRelation.Friendly)
            return true;

        if (DamageEnemy && relation == ETeamRelation.Enemy)
            return true;

        if (DamageNeutral && relation == ETeamRelation.Neutral)
            return true;

        return false;
    }

}
