using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public interface IBehaviourTreeInterface 
{
    //rotate to look a target
    public void RotateTowards(GameObject target, bool verticalAim = false);
}
