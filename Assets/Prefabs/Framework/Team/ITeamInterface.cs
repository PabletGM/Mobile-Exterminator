using System.Collections;
using System.Collections.Generic;
using UnityEngine;


public enum ETeamRelation
{
    Friendly,
    Enemy,
    Neutral
}
public interface ITeamInterface
{
    public int GetTeamID() { return -1; }
    public ETeamRelation GetRelationTowards(GameObject other)
    {
        ITeamInterface otherTeaminterface = other.GetComponent<ITeamInterface>();
        
        if(otherTeaminterface == null)
            return ETeamRelation.Neutral;

        if(otherTeaminterface.GetTeamID() == GetTeamID())
            return ETeamRelation.Friendly;

        else if(otherTeaminterface.GetTeamID() == -1 || GetTeamID() == -1)
            return ETeamRelation.Neutral;

        return ETeamRelation.Enemy;
    }
}
