using System.Collections;
using System.Collections.Generic;
using UnityEngine;

/// <summary>
/// Spawns objects over time and pools them for re-use
/// Assign which object in the editor, takes a scriptable object
/// Attach this script to an empty GameObject
/// </summary>

public class ObjectPoolSpawner : MonoBehaviour
{
    [SerializeField]
    private ObjectPoolData objectPoolData;

    [SerializeField]
    Vector3[] objectPositionField;

    public ObjectPoolData ObjectPoolData { get => objectPoolData; }

    public int currentAlive;

    private void OnEnable()
    {
        objectPositionField = new Vector3[objectPoolData.ObjectAmountMax];

        RandomPlacement();

        StartCoroutine(spawnerOverTime());
    }

    void RandomPlacement()
    {
        for (int i = 0; i < objectPoolData.ObjectAmountMax; i++)
        {
            int randomObjectType = 0;

            float minXRange = transform.position.x - objectPositionField[0].x;
            float maxXRange = transform.position.x + objectPositionField[1].x;

            float minZRange = transform.position.z - objectPositionField[0].z;
            float maxZRange = transform.position.z + objectPositionField[1].z;

            Vector3 spawnRange = new Vector3(Random.Range(minXRange, maxXRange), 0.2f, Random.Range(minZRange, maxZRange));
            objectPositionField[i] = spawnRange;

            Instantiate(objectPoolData.ObjectTypes[randomObjectType], objectPositionField[i], Quaternion.identity, transform);
            currentAlive += 1;


        }
    }

    public Queue<GameObject> itemQueue = new Queue<GameObject>();

    public void Queuer(GameObject objectToQueue) 
    {        
        itemQueue.Enqueue(objectToQueue);
        currentAlive -= 1;
    }

    public void DeQueuer()
    {
        itemQueue.Peek().SetActive(true);
        itemQueue.Dequeue();
        currentAlive += 1;
    }

    IEnumerator spawnerOverTime()
    {
        while (true)
        {
            if (currentAlive < objectPoolData.ObjectAmountMax)
            {
                yield return new WaitForSeconds(4f / objectPoolData.ObjectAmountMax);
                DeQueuer();
            }
            yield return null;
        }
    }
}
