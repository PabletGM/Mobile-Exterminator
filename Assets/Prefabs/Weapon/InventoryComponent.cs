using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class InventoryComponent : MonoBehaviour
{
    //array of prefab weapons that charge at the beggining of the game
    [SerializeField] Weapon[] initWeaponPrefabs;

    //the default weapon slot or position for a particular weapon
    [SerializeField] Transform defaultWeaponSlot;
    //all the weaponSlots that exists
    [SerializeField] Transform[] weaponSlots;

    //this has to be modificable, the actual weapons you have or posses.
    List<Weapon> weapons;
    int currentWeaponIndex = -1;

    private void Start()
    {
        InitializeWeapons();
        
    }

    private void InitializeWeapons()
    {
        //initialize list
        weapons = new List<Weapon>();
        //check all the weapon list
        foreach(Weapon weapon in initWeaponPrefabs)
        {
            //put the defaultWeaponSlot
            Transform weaponSlot = defaultWeaponSlot;
            //we check all the weaponSlots
            foreach (Transform slot in weaponSlots)
            {
                //check the tag of the weaponSlot, if it is the attach tag, it is the correct and we select it
                if (slot.gameObject.tag == weapon.GetAttachSlotTag())
                {
                    weaponSlot = slot;
                }
            }
            //we create the weapon and Instantiate the weapon on the weaponSlot
            Weapon newWeapon = Instantiate(weapon, weaponSlot);
            //we can assign the owner(the gameObject that this component belongs to)
            newWeapon.Init(gameObject);
            //add it to the actual list of weapons having
            weapons.Add(newWeapon);
        }
        //will add the next weapon on the list by default and set it to active
        NextWeapon();
    }

    public void NextWeapon()
    {
        //to be 0
        int nextWeaponIndex = currentWeaponIndex + 1;
        //it means you surpass the limit
        if(nextWeaponIndex >= weapons.Count)
        {
            nextWeaponIndex = 0;
        }

        EquipWeapon(nextWeaponIndex);
    }

    private void EquipWeapon(int weaponIndex)
    {
        //is there Space?
        if (weaponIndex < 0 || weaponIndex >= weapons.Count)
            return;

        //if there is a previous active weapon we have to unequip it
        if(currentWeaponIndex >= 0 && currentWeaponIndex < weapons.Count)
        {
            weapons[currentWeaponIndex].UnEquip();
        }

        //equip the weapon
        weapons[weaponIndex].Equip();
        //we put the active weapon index
        currentWeaponIndex = weaponIndex;
    }

    internal Weapon GetActiveWeapon()
    {
        return weapons[currentWeaponIndex];
    }
}
