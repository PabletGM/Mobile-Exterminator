using System;
using System.Collections;
using System.Collections.Generic;
using Unity.VisualScripting;
using UnityEngine;

//A behaviour sense that knows when it is hit and has their own ForgetStimuli
public class HitSense : SenseComponent
{
    //we need the healthComponent reference to call the event
    [SerializeField] HealthComponent healthComponent;
    //how long the enemy will remember the pain of the damage
    [SerializeField] float HitMemory;
    //HitRecord is assumed to be a Dictionary<PerceptionStimuli, Coroutine> that keeps track of stimuli and their associated forgetting coroutines.
    Dictionary<PerceptionStimuli, Coroutine> HitRecord = new Dictionary<PerceptionStimuli, Coroutine>();
    protected override bool IsStimuliSensable(PerceptionStimuli stimuli)
    {
        //check if we have the stimuli on HitRecord
        return HitRecord.ContainsKey(stimuli);
    }

    // Start is called before the first frame update
    void Start()
    {
        //we add the method TookDamage to the event onTakeDamage
        healthComponent.onTakeDamage += TookDamage;
    }


    //==============================================================================================================================================

    //This TookDamage method and its related coroutine, ForgetStimuli, manage how an object "remembers"
    //and eventually "forgets" which other objects (or entities) have recently caused it damage.


    //the GameObject Instigator is who is causing the damage in this case, it it called on the event automatically
    private void TookDamage(float health, float amount, float maxHealth, GameObject instigator)
    {
        //we check the perception stimuli of the object that hit us
        PerceptionStimuli stimuli = instigator.GetComponent<PerceptionStimuli>();
        //if it exists
        if (stimuli !=null)
        {
            //start the coroutine
            Coroutine newForgettingCoroutine = StartCoroutine(ForgetStimuli(stimuli));
            //If the stimuli is already in HitRecord dictionary, the player attack the enemy more than 1 time.
            if (HitRecord.TryGetValue(stimuli, out Coroutine onGoingCoroutine))
            {
                //The method stops the ongoing forgetting coroutine for this stimuli
                StopCoroutine(onGoingCoroutine);
                //It then updates HitRecord with the new coroutine (newForgettingCoroutine), effectively resetting the forget timer and adding more seconds of remember.
                HitRecord[stimuli] = newForgettingCoroutine;
            }
            //If the stimuli is not in HitRecord
            else
            {
                //The new coroutine is added to HitRecord, starting a fresh forget timer for this stimulus.
                HitRecord.Add(stimuli, newForgettingCoroutine);
            }
        }
    }

    //coroutine where you forget the hit damage
   IEnumerator ForgetStimuli(PerceptionStimuli stimuli)
   {
      yield return new WaitForSeconds(HitMemory);
        //After the wait, the coroutine removes stimuli from HitRecord, meaning the object has "forgotten"
        //that this stimulus recently caused damage.
        HitRecord.Remove(stimuli);
   }


    //==============================================================================================================================================
}
