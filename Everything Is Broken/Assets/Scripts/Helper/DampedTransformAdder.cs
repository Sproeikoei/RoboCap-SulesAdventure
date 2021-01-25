using UnityEngine;
using UnityEditor;
using UnityEngine.Animations.Rigging;

/// <summary>
/// Since it's a hassle to manually add all constraints and their targets, place this script on the constrained Object/Bone
/// Creates the constraint in the corresponding rig you've created with Unity's Animation package
/// </summary>

//[ExecuteInEditMode]
public class DampedTransformAdder : MonoBehaviour
{
    public GameObject RigParent;
    public Transform ObjectParent;
    public GameObject dampedObject;
    public DampedTransform dampedTransformConstraint;

    public Transform ConstrainedObject;
    public Transform SourceObject;

    private void Awake()
    {
        ObjectParent = this.transform.parent;
        //CreateConstraints();
    }

    public void CreateConstraints()
    {
        dampedObject = new GameObject();

        dampedTransformConstraint = dampedObject.AddComponent<DampedTransform>();
        dampedTransformConstraint.data.constrainedObject = this.transform;
        dampedTransformConstraint.data.sourceObject = ObjectParent;

        ConstrainedObject = dampedTransformConstraint.data.constrainedObject;
        SourceObject = dampedTransformConstraint.data.sourceObject;

        dampedTransformConstraint.weight = .5f;
        dampedTransformConstraint.data.dampPosition = 0.1f;
        dampedTransformConstraint.data.dampRotation = 0.1f;
        dampedTransformConstraint.data.maintainAim = true;

        dampedObject.transform.SetParent(RigParent.transform);
    }
}