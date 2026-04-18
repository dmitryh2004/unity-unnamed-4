using UnityEngine;

[CreateAssetMenu(fileName = "Entity values", menuName = "Scriptable Objects/Entity Values")]
public class EntityValues : ScriptableObject
{
    public EntityID entityID;
    public float startHealth;
    public float baseMaxHealth;

    public float startEnergy;
    public float baseMaxEnergy;

    public float baseMovementSpeed;
}
