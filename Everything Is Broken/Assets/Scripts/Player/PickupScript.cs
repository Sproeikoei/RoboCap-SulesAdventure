using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PickupScript : MonoBehaviour
{
    [SerializeField] RoboArmScript roboArmScript;

    private void OnCollisionEnter(Collision collision)
    {
        if (roboArmScript.childrenRenderers.Count != 0)
        {
            roboArmScript.TurnOnChildren();
        }
    }    
}
