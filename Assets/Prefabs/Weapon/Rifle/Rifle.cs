using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Rifle : Weapon
{
    [SerializeField] AimComponent aimComp;
    [SerializeField] float Damage = 5f;
    public override void Attack()
    {
        //get the object the player is aiming
        GameObject target = aimComp.GetAimTarget();
        DamageGameObject(target, Damage);
    }

}
