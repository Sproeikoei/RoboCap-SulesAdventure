using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.AI;

public class Destructable : MonoBehaviour, IDestructable
{
    public DestructableData DestructableData { get => data; }

    public Transform player;

    [SerializeField] DestructableData data;
    [SerializeField] int health;
    [SerializeField] NavMeshAgent navMeshAgent;
    [SerializeField] ObjectPoolSpawner objectPoolSpawner;

    Collider collider;

    public void ReceiveDamage(int damage)
    {
        health -= damage;

        if (health == 0 || health == -5)
            Destroy();
    }

    public void Destroy()
    {
        objectPoolSpawner.Queuer(gameObject);
        gameObject.SetActive(false);
    }

    private void OnEnable()
    {
        health = data.health;

        StartCoroutine(delayStart());
    }

    void Start()
    {
        health = data.health;
        collider = GetComponent<Collider>();

        player = GameObject.Find("Player").transform;
        objectPoolSpawner = transform.parent.GetComponent<ObjectPoolSpawner>();
    }

    IEnumerator delayStart()
    {
        yield return new WaitUntil(() => RoboArmScript.Started);
        navMeshAgent.destination = player.transform.position;

        while (navMeshAgent.destination != player.transform.position && RoboArmScript.Started)
        { 
            yield return new WaitUntil(() => navMeshAgent.remainingDistance < 0.1f);

            navMeshAgent.destination = player.transform.position;
        } 
    }
}
