using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

//You can access it because it is abstract

//base class of the senses with ForgettingRoutines and hereditary behaviour
public abstract class SenseComponent : MonoBehaviour
{
    // registeredStimulis will be shared across all instances of the class in which it's declared,
    // allowing any object to access it directly through the class without needing an instance.

    //list with all the GameObjects that can be attackable or registered by the AI
    static List<PerceptionStimuli> registeredStimulis = new List<PerceptionStimuli>();
    //list of perceived stimulis
    List<PerceptionStimuli> PerceivableStimuli = new List<PerceptionStimuli>();
    //creates a dictionary that serves as a data structure for managing active forgetting routines associated with each PerceptionStimuli object.
    //ForgettingRoutines: This is the dictionary where keys are PerceptionStimuli and values are Coroutine objects.
    Dictionary<PerceptionStimuli, Coroutine> ForgettingRoutines = new Dictionary<PerceptionStimuli, Coroutine>();

    public delegate void OnPerceptionUpdated(PerceptionStimuli stimuli, bool successfullySensed);

    public event OnPerceptionUpdated onPerceptionUpdated;
    
    [SerializeField]
    float forgettingTime = 3f;

    //adds all the senses to the list of registeredSenses
    static public void RegisterStimuli(PerceptionStimuli stimuli)
    {
        if(registeredStimulis.Contains(stimuli))
        {
            return;
        }
        //if it is not in the list, we add it
        registeredStimulis.Add(stimuli);
    }


    //removes all the senses to the list of registeredSenses
    static public void UnRegisterStimuli(PerceptionStimuli stimuli)
    {
        registeredStimulis.Remove(stimuli);
    }


    //=======================================================================================================
   

    //an obligatory method that their children will have
    protected abstract bool IsStimuliSensable(PerceptionStimuli stimuli);
   

    //=========================================================================================================


    // Update is called once per frame
    void Update()
    {
        //it goes to each sense
        foreach(var stimuli in registeredStimulis)
        {
            //is the stimuli being sensed, ask the children classes senses, if yes
            if(IsStimuliSensable(stimuli))
            {
                //if it has not been perceived
                if (!PerceivableStimuli.Contains(stimuli))
                {
                    PerceivableStimuli.Add(stimuli);
                    //if it has been an iniciated coroutine of Forgetting and we are perceived again, we stop that coroutine
                    //This method attempts to retrieve(recuperar) the value associated with the key stimuli from the ForgettingRoutines dictionary.
                    //This method returns true if the key exists in the dictionary and retrieves the associated value(coroutine). If the key does not exist, it returns false.
                    if (ForgettingRoutines.TryGetValue(stimuli, out Coroutine routine))
                    {
                        //stops coroutine
                        StopCoroutine(routine);
                        //removes the routine from the dictionary
                        ForgettingRoutines.Remove(stimuli);
                    }
                    //if the forgetting coroutine doesnt exist
                    else
                    {
                        //check if there is any suscribe method to the event onPerceptionUpdated and invokes them with true parameter
                        onPerceptionUpdated?.Invoke(stimuli, true);
                    }
                   
                }
            }
            //not being sensed
            else
            {
                //BUT perceived
                if(PerceivableStimuli.Contains(stimuli))
                {
                    PerceivableStimuli.Remove(stimuli);
                    //adding to the dictionary the stimuli and the corroutine forget.
                    ForgettingRoutines.Add(stimuli, StartCoroutine(ForgetStimuli(stimuli)));

                }
            }
        }
    }

    protected virtual void DrawDebug()
    {

    }

    private void OnDrawGizmos()
    {
        DrawDebug();
    }


    //================================================================================================

    
    // Coroutine to forget a stimulus after 5 seconds
    public IEnumerator ForgetStimuli(PerceptionStimuli stimuli)
    {
        yield return new WaitForSeconds(forgettingTime); // Waits for 5 seconds
        //takes out the forgetting routine of the dictionary
        ForgettingRoutines.Remove(stimuli);
        //now, it is not percepted
        //check if there is any suscribe method to the event onPerceptionUpdated and invokes them with false parameter
        onPerceptionUpdated?.Invoke(stimuli, false);

        
    }

    //add a new perception stimuli and call the methods when the perception changes
    internal void AssignPerceivedStimuli(PerceptionStimuli targetStimuli)
    {
        //add the stimuli
        PerceivableStimuli.Add(targetStimuli);
        //call the event
        onPerceptionUpdated?.Invoke(targetStimuli, true);

        //TO DO -> WHAT IF WE ARE FORGETTING because we dont have the player near enough
        //if(ForgettingRoutines.TryGetValue(targetStimuli, out Coroutine forgetCoroutine))
        //{
        //    //we stop that forget coroutine
        //    StopCoroutine(forgetCoroutine);
        //    ForgettingRoutines.Remove(targetStimuli);
        //}
        
    }
}
