using System.Collections;
using System.Collections.Generic;
using UnityEngine;

//The objects that have this class will be perceived by the enemies or AI(for now, only Player)
public class PerceptionStimuli : MonoBehaviour
{
    //Add new Register stimuli to the AI list
    void Start()
    {
        //we add this sense to the list 
        SenseComponent.RegisterStimuli(this);
    }

    //Remove the Register stimuli to the AI list
    private void OnDestroy()
    {
        //unregister if destroyed
        SenseComponent.UnRegisterStimuli(this);
    }
}
