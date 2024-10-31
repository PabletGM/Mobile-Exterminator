using System.Collections;
using System.Collections.Generic;
using UnityEngine;

//You can access it because it is abstract
public abstract class SenseComponent : MonoBehaviour
{
    // registeredStimulis will be shared across all instances of the class in which it's declared,
    // allowing any object to access it directly through the class without needing an instance.
    static List<PerceptionStimuli> registeredStimulis = new List<PerceptionStimuli>();
    //list of perceived stimulis
    List<PerceptionStimuli> PerceivableStimuli = new List<PerceptionStimuli>();
    //creates a dictionary that serves as a data structure for managing active forgetting routines associated with each PerceptionStimuli object.
    //ForgettingRoutines: This is the dictionary where keys are PerceptionStimuli and values are Coroutine objects.
    Dictionary<PerceptionStimuli, Coroutine> ForgettingRoutines = new Dictionary<PerceptionStimuli, Coroutine>();
    
    [SerializeField]
    float forgettingTime = 3f;

    static public void RegisterStimuli(PerceptionStimuli stimuli)
    {
        if(registeredStimulis.Contains(stimuli))
        {
            return;
        }
        //if it is not in the list, we add it
        registeredStimulis.Add(stimuli);
    }


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
        foreach(var stimuli in registeredStimulis)
        {
            //is the stimuli being sensed, ask the children senses, if yes
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
                    else
                    {
                        Debug.Log($"I just sensed {stimuli.gameObject}");
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
        
        ForgettingRoutines.Remove(stimuli);    

        Debug.Log($"I lost track of sensed {stimuli.gameObject}");
    }
}
