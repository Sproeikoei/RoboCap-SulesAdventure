using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.InputSystem;

/// <summary>
/// Attach to the object you wish to animate
/// Constant Roll rotation (Z axis)
/// On Left Click 180 Pitch rotation (X axis) and 360 Yaw rotation (Y axis)
/// </summary>

public class ToolAnimation : MonoBehaviour
{
    public float SequenceValue
    {
        get { return _sequenceValue; }
        set { _sequenceValue = Mathf.Clamp(value, 0, 1); }
    }

    public Transform parentTransform;

    public AnimationCurve yCurve;
    public AnimationCurve curve;
    public float rotationSpeed = 1f;
    public float lerpTime = 1f;

    [Range(0, 1)]
    [SerializeField] float _sequenceValue;
    [SerializeField] float lerpRatio;

    public static bool attacking = true;
    float timer;

    void Update()
    {
        ConstantZRotation();
    }

    Vector3 ConstantZRotation()
    {
        if (timer > lerpTime)
            timer = 0;
        else
            timer += (Time.unscaledDeltaTime * 0.2f);

        lerpRatio = timer / lerpTime;

        Vector3 rotation = Vector3.forward * rotationSpeed;
        Vector3 lerpedRotation = curve.Evaluate(lerpRatio) * rotation;


        transform.Rotate(lerpedRotation, Space.Self);
        return lerpedRotation;
    }

    public void RotateCharacter()
    {
        StartCoroutine(Co_RotateChar());
    }

    IEnumerator Co_RotateChar()
    {
        attacking = !attacking;

        while (     SequenceValue <= 1 
                &&  SequenceValue >= 0)
        {
            float lerpedRotation = 180f * SequenceValue * rotationSpeed;
            Vector3 attackingRotation = new Vector3(lerpedRotation * 2, lerpedRotation, ConstantZRotation().z);

            if (!attacking)
            {
                SequenceValue -= yCurve.Evaluate(Time.deltaTime * 2f);
                transform.localEulerAngles = attackingRotation;
            }
            else if (attacking)
            {
                SequenceValue += yCurve.Evaluate(Time.deltaTime * 2f);
                transform.localEulerAngles = attackingRotation;
            }
            yield return null;

            if (SequenceValue == 0 || SequenceValue == 1)
                yield break;
        }
    }
}
