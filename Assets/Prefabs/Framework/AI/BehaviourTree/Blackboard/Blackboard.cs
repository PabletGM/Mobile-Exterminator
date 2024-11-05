using System.Collections;
using System.Collections.Generic;
using UnityEngine;

//Knowledge that AI should know
public class Blackboard 
{
   Dictionary<string, object> blackboardData = new Dictionary<string, object>();

   public delegate void OnBlackboardValueChange(string key, object value);
   public event OnBlackboardValueChange onBlackboardValueChange;

    public void SetOrAddData(string key, object value)
    {
        //if it has the key, give the value
        if(blackboardData.ContainsKey(key))
        {
            blackboardData[key] = value;
        }
        //if it doesnt have the key, give the key and value
        else
        {
            blackboardData.Add(key, value);
        }
        //we invoke all the methods suscribed to this event
        onBlackboardValueChange?.Invoke(key, value);
    }

    //we want to store any type of datas. GameObjects, Vector3, Target, etc
    //If the key exists, it attempts to cast the object stored to
    //type T and returns true, storing the value in val.
    public bool GetBlackboardData<T>(string key, out T val)
    {
        //we take the generic value
        val = default(T);
        //we ask to the dictionary if it has the key
        if(blackboardData.ContainsKey(key))
        {
            val = (T)blackboardData[key];
            return true;
        }
        return false;
    }

    public void RemoveBlackboardData(string key)
    {
        if(blackboardData.ContainsKey(key))
        {
            blackboardData.Remove(key);
            onBlackboardValueChange?.Invoke(key,null);
        }
    }

    public bool HasKey(string key)
    {
        return blackboardData.ContainsKey(key);
    }
}
