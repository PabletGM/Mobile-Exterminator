using System;
using System.Collections;
using System.Collections.Generic;
using Unity.VisualScripting;
using UnityEditor.Experimental.GraphView;
using UnityEngine;

/// <summary>
/// What senses are we currently sensing and which one is the highest priority or the target to attack.
/// What behaviour senses( always aware, sight, hit...) the AI has.
/// </summary>
public class PerceptionComponent : MonoBehaviour
{
    //We have to keep Count of all the Senses, and each sense must have a number
    [SerializeField] SenseComponent[] senses;

    //We will use LinkedList:

    //A LinkedList is a data structure that consists of a sequence of elements called nodes, where each node contains:
    //   -Data: The actual content or value of the node.
    //   -Pointer(or Reference): A link to the next node in the sequence.
    LinkedList<PerceptionStimuli> currentlyPerceivedStimulis = new LinkedList<PerceptionStimuli>();

    //the target of the AI to focus if there is more than 1.
    PerceptionStimuli targetStimuli;

    //event when change the target
    public delegate void OnPerceptionTargetChanged(GameObject target, bool sensed);
    public event OnPerceptionTargetChanged onPerceptionTargetChanged;

    private void Start()
    {
        foreach(SenseComponent sense in senses)
        {
            //add OnPerceptionUpdated method to the event onPerceptionUpdated
            sense.onPerceptionUpdated += SenseUpdated;
        }
    }


    //This SenseUpdated method manages the addition and removal of a stimuli node in a LinkedList
    //called currentlyPerceivedStimulis based on whether the stimuli was successfully sensed or not.
    private void SenseUpdated(PerceptionStimuli stimuli, bool successfullySensed)
    {
        // Find is usually used with a predicate function (e.g., Find(s => s.Equals(stimuli))) to locate a specific item in the list.
        //find the stimuli node
        var nodeFound = currentlyPerceivedStimulis.Find(stimuli);

        //if the stimuli was sensed we add it 
        if (successfullySensed)
        {           
            //if it exists
            if(nodeFound != null)
            {
                //is attempting to add a new stimuli node immediately after an existing node (nodeFound)
                currentlyPerceivedStimulis.AddAfter(nodeFound, stimuli);
            }
            //if it doesnt exist
            else
            {
                //it adds the stimuli in the last position
                currentlyPerceivedStimulis.AddLast(stimuli);
            }
        }
        //if the stimuli was not sensed, it is removed
        else
        {
            currentlyPerceivedStimulis.Remove(nodeFound);
        }



        //TO PUT THE PRIORITY PERCEIVED STIMULI ON TARGET STIMULI

        //if there is any perceived stimulis we take the highest priority or first item
        if(currentlyPerceivedStimulis.Count != 0)
        {
            PerceptionStimuli highestStimuli = currentlyPerceivedStimulis.First.Value;
            //if targetStimuli does not exist or is different from highest
            if(targetStimuli == null || targetStimuli != highestStimuli)
            {
                targetStimuli = highestStimuli;
                //if there is any method suscribe to the event onPerceptionTargetChanged, it invokes them with the parameters
                //it changes the target to the first so we call the event
                onPerceptionTargetChanged?.Invoke(targetStimuli.gameObject, true);
            }
        }
        //if there is not anything
        else
        {
            if(targetStimuli != null)
            {
                //it changes the target to null so we call the event
                onPerceptionTargetChanged?.Invoke(targetStimuli.gameObject, false);
                targetStimuli = null;  
            }
        }
    }
}
