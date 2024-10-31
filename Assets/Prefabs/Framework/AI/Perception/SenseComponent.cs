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

    protected abstract bool IsStimuliSensable(PerceptionStimuli stimuli);
    // Start is called before the first frame update
    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {
        foreach(var stimuli in registeredStimulis)
        {
            //is the stimuli being sensed
            if(IsStimuliSensable(stimuli))
            {
                //if it has not been perceived
                if(!PerceivableStimuli.Contains(stimuli))
                {
                    PerceivableStimuli.Add(stimuli);
                    Debug.Log($"I just sensed {stimuli.gameObject}");
                }
            }
            //not being sensed
            else
            {
                //BUT perceived
                if(PerceivableStimuli.Contains(stimuli))
                {
                    //It was an error on detection or it is not currently sensing it
                    PerceivableStimuli.Remove(stimuli);
                    Debug.Log($"I lost track of sensed {stimuli.gameObject}");
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
}
