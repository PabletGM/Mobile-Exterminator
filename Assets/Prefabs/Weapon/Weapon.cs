using System.Collections;
using System.Collections.Generic;
using UnityEngine;

//parent class that all the weapons will have
public  abstract class Weapon : MonoBehaviour
{
    //each weapon has a tag
    [SerializeField] string AttachSlotTag;

    //each weapon has a tag
    [SerializeField] float AttackRateMult = 1f;

    //each weapon has a AnimationController overriding some basic animations
    [SerializeField] AnimatorOverrideController overrideController;

    //abstract function because we want that the children has access and override it
    public abstract void Attack();
    public string GetAttachSlotTag()
    { 
        return AttachSlotTag; 
    }

   public GameObject Owner
   {
        get;
        private set;
   }

    //To initialise as the owner a GameObject
   public void Init(GameObject owner)
   {
        Owner = owner;
        //we are not holding the weapon yet
        //if not all the weapons will be activated by default
        UnEquip();
   }

   public void Equip()
   {
        gameObject.SetActive(true);
        Owner.GetComponent<Animator>().runtimeAnimatorController =overrideController;
        Owner.GetComponent <Animator>().SetFloat("AttackRateMult", AttackRateMult);
   }

    public void UnEquip()
    {
        gameObject.SetActive(false);
    }

    public void DamageGameObject(GameObject objToDamage, float amount)
    {
        //see if it exists healthComponent
        HealthComponent healthComp = objToDamage.GetComponent<HealthComponent>();
        if (healthComp != null)
        {
            //make change Health
            healthComp.changeHealth(-amount, Owner);
        }
    }
}
