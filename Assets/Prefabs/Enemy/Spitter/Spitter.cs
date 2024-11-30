using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Spitter : Enemy
{
    [SerializeField] Projectile projectilePrefab;
    [SerializeField] Transform launchPoint;

    Vector3 Destination;

    [SerializeField] VFXSpecification[] DeathVFX;
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

    public override void AttackTarget(GameObject target)
    {
        Animator.SetTrigger("Attack");
        Destination = target.transform.position;
    }

    //animation event on spitter attack distance
    public void AttackPointShoot()
    {
        Shoot();
    }

    public void Shoot()
    {
        //we create and instantiate the projectile at an specific point and rotation
        Projectile newProjectile = Instantiate(projectilePrefab, launchPoint.position, launchPoint.rotation);
        newProjectile.Launch(this.gameObject, Destination);
    }

    
}
