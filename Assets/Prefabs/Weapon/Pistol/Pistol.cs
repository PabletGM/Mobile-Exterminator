using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Pistol : Weapon
{
    [SerializeField] AimComponent aimComp;
    [SerializeField] float Damage = 10f;
    public override void Attack()
    {
        //get the object the player is aiming
        GameObject target = aimComp.GetAimTarget();
        DamageGameObject(target, Damage);
    }
}
