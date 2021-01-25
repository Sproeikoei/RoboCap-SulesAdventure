using UnityEngine;

[CreateAssetMenu(fileName = "New Objectpool Data", menuName = "New Objectpool Data")]
public class ObjectPoolData : ScriptableObject
{
    [SerializeField]
    GameObject[] objectTypes;
    [SerializeField]
    int objectAmountMax;
    [SerializeField]
    int objectCurrentAmount;
    [SerializeField]
    int objectMaxScale;

    public GameObject[] ObjectTypes { get => objectTypes; }
    public int ObjectAmountMax { get => objectAmountMax; }
    public int ObjectCurrentAmount { get => objectCurrentAmount; set => objectCurrentAmount = value; }
    public int ObjectMaxScale { get => objectMaxScale; }
}
