using System;
using System.Collections.Generic;
using UnityEngine;

public enum MoveDirectionsCount
{
    Two = 2,
    Four = 4
}
public enum EntityID
{
    player = 1,
    wolf = 2
}
public class Entity : MonoBehaviour, IDamagable, IEffectable, IMovable
{
    [SerializeField] EntityID entityID;
    [SerializeField] Rigidbody2D rb;
    [Header("Sprite sheet")]
	[SerializeField] protected Animator animator;
    [SerializeField] MoveDirectionsCount moveDirectionsCount;

    [Header("Weapon")]
    public BaseWeapon weapon;
    public Transform weaponObject;
    public Vector2 weaponDirection;

    EntityValues entityValues = null;
    private float health;
    private float baseMaxHealth;
    private float currentMaxHealth;
    private float energy;
    private float baseMaxEnergy;
    private float currentMaxEnergy;
    private float baseSpeed;

    private bool moving;
    private Vector2 direction;
    private Vector2 facingDirection;

    private Dictionary<EffectID, int> effectCount = new();
    private Dictionary<EffectID, float> effectRemainingTime = new();

    #region Реализация Effectable
    public virtual void AddEffect(EffectID effectID, int count = 1)
    {
        int maxCount = EffectManager.Instance.GetEffectValuesByID(effectID).maxCount;
        effectCount[effectID] += count;
        if (effectCount[effectID] > maxCount)
            effectCount[effectID] = maxCount;
        effectRemainingTime[effectID] = EffectManager.Instance.GetEffectValuesByID(effectID).duration;
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

    public virtual void RemoveEffect(EffectID effectID, int count = 1)
    {
        effectCount[effectID] -= count;
        if (effectCount[effectID] < 0) effectCount[effectID] = 0;
        if (effectCount[effectID] > 0) effectRemainingTime[effectID] = EffectManager.Instance.GetEffectValuesByID(effectID).duration;
    }

    public virtual void RemoveAllEffectOfType(EffectID effectID)
    {
        effectCount[effectID] = 0;
    }

    public virtual void RemoveAllEffects()
    {
        foreach(var effect in effectCount.Keys) {
            effectCount[effect] = 0;
        }
    }
    #endregion

    #region Реализация Damagable

    public bool IsAlive()
    {
        return health > 0;
    }
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

    public float GetCurrentHealth() => health;
    public float GetCurrentEnergy() => energy;
    public bool IsEnoughEnergy(float required) => energy >= required;
    public void UseEnergy(float energy)
    {
        if (IsEnoughEnergy(energy))
        {
            this.energy -= energy;
        }
    }
    public void AddEnergy(float energy)
    {
        this.energy += energy;
        if (energy > currentMaxEnergy)
        {
            energy = currentMaxEnergy;
        }
    }

    protected virtual void OnDeath()
    {

    }
    #endregion

    #region Реализация Movable
    public void Move(Vector2 direction)
    {
        SetDirection(direction.normalized);
        rb.linearVelocity = this.direction * GetCurrentSpeed();
		
		moving = this.direction != Vector2.zero;

        UpdateMoveAnimator();
    }

    public bool IsMoving() => moving;

    public void SetMoving(bool moving) => this.moving = moving;
    public Vector2 CurrentDirection => direction;
    public void SetDirection(Vector2 direction)
    {
        this.direction = direction;
        if (direction != Vector2.zero) facingDirection = direction;
    }

    protected void UpdateMoveAnimator()
    {
        animator.SetBool("moving", moving);

        if (moving)
        {
            int animDirection = 0;
            if (direction.x > 0)
            {
                animDirection = 0;
                if (moveDirectionsCount == MoveDirectionsCount.Four)
                {
                    if (direction.y > direction.x) animDirection = 1;
                    else if (direction.y < -direction.x) animDirection = 3;
                }
            }
            else if (direction.x < 0)
            {
                animDirection = 2;
                if (moveDirectionsCount == MoveDirectionsCount.Four)
                {
                    if (direction.y > -direction.x) animDirection = 1;
                    else if (direction.y < direction.x) animDirection = 3;
                }
            }
            else animDirection = (moveDirectionsCount == MoveDirectionsCount.Four) ? ((direction.y > 0) ? 1 : 3) : 0;

            Debug.Log($"{gameObject.name}: direction = {direction}");

            animator.SetInteger("direction", animDirection);
        }
    }

    public float GetCurrentSpeed()
    {
        float speed = baseSpeed;
        if (HasEffect(EffectID.Dash))
        {
            speed *= EffectManager.Instance.GetEffectValuesByID(EffectID.Dash).GetEffectValue("MovementSpeedModifier") ?? 1f;
        }
        return speed;
    }

    public float GetBaseSpeed()
    {
        return baseSpeed;
    }
    #endregion

    protected virtual void ChangeWeaponPosition()
    {
        if (weaponObject == null) return;
        weaponDirection = facingDirection;
        Vector2 normalizedDirection = weaponDirection.normalized;
        if (normalizedDirection == Vector2.zero)
        {
            normalizedDirection = Vector2.right;
        }
        Vector2 weaponPosition = normalizedDirection * 0.25f;
        weaponObject.localPosition = weaponPosition;
        weaponObject.localRotation = Quaternion.Euler(0f, 0f, Vector2.SignedAngle(Vector2.right, normalizedDirection));
    }

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

        baseMaxEnergy = entityValues.baseMaxEnergy;
        currentMaxEnergy = baseMaxEnergy;
        energy = entityValues.startEnergy;

        baseSpeed = entityValues.baseMovementSpeed;

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

    private void Update()
    {
        if (IsAlive())
        {
            ProcessEffects();
            UpdateEntity();
        }
    }

    protected virtual void UpdateEntity()
    {
        ChangeWeaponPosition();
    }

    private void ProcessEffects()
    {
        foreach (EffectID effectID in EffectManager.Instance.EffectIDs)
        {
            if (effectRemainingTime[effectID] > 0f)
            {
                effectRemainingTime[effectID] -= Time.deltaTime;
                if (effectRemainingTime[effectID] <= 0f)
                {
                    RemoveEffect(effectID);
                }
            }
        }

        // process effect behaviour
    }

    public void TryAttack()
    {
        if (weapon != null)
        {
            weapon.LaunchAttack();
        }
    }
}
