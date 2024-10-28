using System.Collections;
using System.Collections.Generic;
using UnityEngine;

//parent class that all the weapons will have
public  abstract class Weapon : MonoBehaviour
{
    [SerializeField] string AttachSlotTag;

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
   }

   public void Equip()
   {
        gameObject.SetActive(true);
   }

    public void UnEquip()
    {
        gameObject.SetActive(false);
    }
}
