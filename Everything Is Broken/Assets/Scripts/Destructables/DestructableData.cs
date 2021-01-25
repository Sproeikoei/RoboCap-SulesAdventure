using UnityEngine;

[CreateAssetMenu(fileName = "Destructable", menuName = "ScriptableObjects/DestructableObject", order = 1)]
public class DestructableData : ScriptableObject
{
    public string prefabName;

    public int health;
    public int damage;
}