
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.AI;

public class SpawnComponent : MonoBehaviour
{
    [SerializeField] GameObject[] objectsToSpawn;
    [SerializeField] Transform spawnTransform;
    [SerializeField] ParticleSystem spawnerVFXSpawnTransform;
    Animator animator;

    [Header("Creation of PatrolPoints on NavMesh")]
    [SerializeField] float patrolPointRadius = 10.0f;
    [SerializeField] int numberOfPatrolPoints = 3;

    // Preset array of patrol points
    [SerializeField] private Transform[] presetPatrolPoints; // Assign these in the Inspector

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
        // Spawn VFX
        SetVFXSpawn(true);

        // Select a random object to spawn
        int randomPick = Random.Range(0, objectsToSpawn.Length);
        GameObject newSpawn = Instantiate(objectsToSpawn[randomPick], spawnTransform.position, spawnTransform.rotation);

        // Check if there is any patrolPoint and assign random ones
        if (newSpawn.GetComponent<PatrollingComponent>().patrolPoints != null)
        {
            newSpawn.GetComponent<PatrollingComponent>().patrolPoints = GetRandomPatrolPointsFromArray(presetPatrolPoints, numberOfPatrolPoints);
        }

        // Start a coroutine to deactivate the VFX after its duration
        StartCoroutine(DeactivateVFXAfterDuration(spawnerVFXSpawnTransform.main.duration));
    }

    private void SetVFXSpawn(bool set)
    {
        // Activate or deactivate the VFX
        spawnerVFXSpawnTransform.gameObject.SetActive(set);
    }

    private IEnumerator DeactivateVFXAfterDuration(float duration)
    {
        // Wait for the duration of the particle system
        yield return new WaitForSeconds(duration);

        // Deactivate the VFX
        SetVFXSpawn(false);
    }

    private Transform[] GetRandomPatrolPointsFromArray(Transform[] sourcePoints, int count)
    {
        List<Transform> randomPoints = new List<Transform>();
        List<int> usedIndices = new List<int>();

        for (int i = 0; i < count; i++)
        {
            int randomIndex;
            do
            {
                randomIndex = Random.Range(0, sourcePoints.Length);
            } while (usedIndices.Contains(randomIndex)); // Avoid duplicate indices

            usedIndices.Add(randomIndex);
            randomPoints.Add(sourcePoints[randomIndex]);
        }

        return randomPoints.ToArray();
    }
}
