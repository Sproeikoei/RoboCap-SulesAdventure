using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class RoboPartPickup : MonoBehaviour
{
    [SerializeField] GameObject controller;
    [SerializeField] RoboArmScript roboArmScript;
    [SerializeField] ObjectPoolSpawner objectPoolSpawner;

    private void Start()
    {
        objectPoolSpawner = transform.parent.GetComponent<ObjectPoolSpawner>();
        roboArmScript = GameObject.Find("Projectiles Parent").GetComponent<RoboArmScript>();
    }

    private void OnCollisionEnter(Collision collision)
    {
        objectPoolSpawner.Queuer(gameObject);
        if (roboArmScript.childrenRenderers.Count != 0)
        { 
            roboArmScript.TurnOnChildren(); 
        }

        gameObject.SetActive(false);
    }
}
