using System.Collections.Generic;
using Unity.VisualScripting;
using UnityEngine;

public enum EntityID
{
    player = 0,
    testEnemy = 1
}
public class Entity : MonoBehaviour, IDamagable, Effectable
{
    [SerializeField] EntityID entityID;
    EntityValues entityValues = null;
    private float health;
    private float baseMaxHealth;
    private float currentMaxHealth;
    private Dictionary<EffectID, int> effectCount = new();
    private Dictionary<EffectID, float> effectRemainingTime = new();

    #region Реализация Effectable
    public void AddEffect(EffectID effectID, int count = 1)
    {
        effectCount[effectID] += count;
    }

    public int GetEffectCount(EffectID effectID)
    {
        return effectCount[effectID];
    }

    public float GetEffectRemainingTime(EffectID effectID)
    {
        return effectRemainingTime[effectID];
    }

    public bool HasEffect(EffectID effectID)
    {
        return GetEffectCount(effectID) > 0;
    }

    public void RemoveEffect(EffectID effectID, int count = 1)
    {
        effectCount[effectID] -= count;
        if (effectCount[effectID] < 0) effectCount[effectID] = 0;
    }

    public void RemoveAllEffectOfType(EffectID effectID)
    {
        effectCount[effectID] = 0;
    }

    public void RemoveAllEffects()
    {
        foreach(var effect in effectCount.Keys) {
            effectCount[effect] = 0;
        }
    }
    #endregion

    #region Реализация Damagable
    public void Heal(float heal)
    {
        health += heal;
        if (health > currentMaxHealth)
        {
            health = currentMaxHealth;
        }
    }

    public void TakeDamage(float damage)
    {
        health -= damage;
        if (health < 0)
        {
            health = 0;
            OnDeath();
        }
    }

    protected virtual void OnDeath()
    {

    }
    #endregion

    void Awake()
    {
        if (EntityValuesManager.Instance == null)
        {
            Debug.LogError($"{name}: entity values manager not found");
            Destroy(gameObject);
            return;
        }
        entityValues = EntityValuesManager.Instance.GetEntityValuesByID(entityID);
        if (entityValues == null)
        {
            Debug.LogError($"{name}: entity values for entity with id = {entityID} not found");
            Destroy(gameObject);
            return;
        }
        baseMaxHealth = entityValues.baseMaxHealth;
        currentMaxHealth = baseMaxHealth;
        health = entityValues.startHealth;

        if (EffectManager.Instance == null)
        {
            Debug.LogError($"{name}: effect manager not found");
            Destroy(gameObject);
            return;
        }
        for (int i = 0; i < EffectManager.Instance.GetEffectTypeCount(); i++)
        {
            effectCount[EffectManager.Instance.GetEffectValuesByIndex(i).effectID] = 0;
            effectRemainingTime[EffectManager.Instance.GetEffectValuesByIndex(i).effectID] = 0f;
        }
    }
}
