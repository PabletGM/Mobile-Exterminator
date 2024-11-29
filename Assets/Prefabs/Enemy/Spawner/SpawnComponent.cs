
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.AI;

public class SpawnComponent : MonoBehaviour
{
    [SerializeField] GameObject[] objectsToSpawn;
    [SerializeField] Transform spawnTransform;
    Animator animator;

    [Header("Creation of PatrolPoints on NavMesh")]
    [SerializeField] float patrolPointRadius = 10.0f;
    [SerializeField] int numberOfPatrolPoints = 3;
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
       
        //check if there is any patrolPoint
        if(newSpawn.GetComponent<PatrollingComponent>().patrolPoints!=null)
        {
            newSpawn.GetComponent<PatrollingComponent>().patrolPoints = GenerateRandomPatrolPoints(numberOfPatrolPoints, patrolPointRadius);
        }
    }

    private Transform[] GenerateRandomPatrolPoints(int count, float radius)
    {
        List<Transform> patrolPoints = new List<Transform>();

        for (int i = 0; i < count; i++)
        {
            Vector3 randomPoint = GetRandomNavMeshPoint(spawnTransform.position, radius);
            if (randomPoint != Vector3.zero)
            {
                // Create a new GameObject to represent the patrol point
                GameObject patrolPoint = new GameObject($"PatrolPoint_{i}");
                patrolPoint.transform.position = randomPoint;
                patrolPoints.Add(patrolPoint.transform);
            }
        }

        return patrolPoints.ToArray();
    }

    private Vector3 GetRandomNavMeshPoint(Vector3 center, float radius)
    {
        Vector3 randomDirection = Random.insideUnitSphere * radius;
        randomDirection += center;

        NavMeshHit hit;
        if (NavMesh.SamplePosition(randomDirection, out hit, radius, NavMesh.AllAreas))
        {
            return hit.position;
        }

        // Return zero vector if no valid point is found
        return Vector3.zero;
    }
}
