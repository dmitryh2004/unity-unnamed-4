using UnityEngine;

[CreateAssetMenu(fileName = "EntityValues", menuName = "Scriptable Objects/EntityValues")]
public class EntityValues : ScriptableObject
{
    public EntityID entityID;
    public float startHealth;
    public float baseMaxHealth;

    public float baseMovementSpeed;
}
