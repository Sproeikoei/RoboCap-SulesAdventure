using System.Collections;
using System.Collections.Generic;
using UnityEditor;
using UnityEngine;
using UnityEngine.Animations.Rigging;

public class ArmCollider : MonoBehaviour
{
    [SerializeField] Controller parentControllerScript;
    [SerializeField] RoboArmScript roboArmScript;
    [SerializeField] DampedTransform dampedTransformConstraint;
    [SerializeField] Transform parentCapsule;

    Rigidbody rigidBody;

    [SerializeField] int health;
    int damage = 5;

    private void OnEnable()
    {
        health = 75;
    }

    private void Start()
    {
        parentCapsule = this.transform;
        rigidBody = gameObject.GetComponent<Rigidbody>();
        rigidBody.isKinematic = true;
        parentControllerScript = transform.root.GetComponent<Controller>();       
    }

    private void OnCollisionEnter(Collision collision)
    {
        TakeDamage();

        IDestructable destructable = collision.gameObject.GetComponent<IDestructable>();
        if (destructable != null)
        {
            DealDamage(destructable, damage);
        }
    }

    void TakeDamage()
    {
        if (RoboArmScript.Started)
        {
            if (ToolAnimation.attacking)
                damage = 10;
            else
                damage = 5;

            health -= damage;
            if (health == 0 || health == -5)
            {
                parentControllerScript.SpawnRoboPartAtCollision();
                roboArmScript.TurnOffChildren(parentCapsule);
            }
        }
    }

    void DealDamage(IDestructable destructable, int damage)
    {
        destructable.ReceiveDamage(damage);
    }
}