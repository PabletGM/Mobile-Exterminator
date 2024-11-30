using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;



//create a class for VFX specifications
[Serializable]
public class VFXSpecification
{
    public ParticleSystem particleSystem;
    public float size;
}
public class Spawner : Enemy
{
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
}
