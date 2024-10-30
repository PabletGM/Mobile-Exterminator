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
        // output parameter aimDir, which will store the aiming direction calculated in the method.
        GameObject target = aimComp.GetAimTarget(out Vector3 aimDir);
        DamageGameObject(target, Damage);

        //we put the bulletVFX rotation with the aimDir
        bulletVFX.transform.rotation = Quaternion.LookRotation(aimDir);
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
