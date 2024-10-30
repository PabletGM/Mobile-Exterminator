using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class RangedWeapons : Weapon
{
    [SerializeField] AimComponent aimComp;
    [SerializeField] float Damage = 5f;
    [SerializeField] ParticleSystem bulletVFX;

    public override void Attack()
    {
        //get the object the player is aiming
        GameObject target = aimComp.GetAimTarget();
        DamageGameObject(target, Damage);

        //we emit the emission burst value of the particle system editor 
        bulletVFX.Emit(bulletVFX.emission.GetBurst(0).maxCount);
    }

    // Start is called before the first frame update
    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {
        
    }
}
