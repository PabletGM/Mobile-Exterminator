
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class SpawnComponent : MonoBehaviour
{
    [SerializeField] GameObject[] objectsToSpawn;
    [SerializeField] Transform spawnTransform;
    Animator animator;
    // Start is called before the first frame update
    void Start()
    {
        animator = GetComponent<Animator>();
    }

    public bool StartSpawn()
    {
        if(objectsToSpawn.Length == 0) return false;

        if(animator!=null)
        {
            animator.SetTrigger("Spawn");
        }
        //if it is null it can make animation events and cannot call Spawn method
        else
        {
           SpawnImplementation();
        }
        

        return true;
    }

    public void SpawnImplementation()
    {
       int randomPick = Random.Range(0, objectsToSpawn.Length);
       GameObject newSpawn = Instantiate(objectsToSpawn[randomPick], spawnTransform.position, spawnTransform.rotation);
    }
}
