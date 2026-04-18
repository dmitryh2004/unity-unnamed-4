using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class BaseWeapon : MonoBehaviour
{
    [SerializeField] protected WeaponID weaponID;
    protected float basePrepareAttackTime;
    protected float baseEndAttackTime;
    protected float baseDamage;
    protected float baseReloadTime;
    [SerializeField] protected List<string> targetTags;
    [SerializeField] protected Entity owner;
    protected int energyUsage;
    protected float prepareAttackTimeModifier = 1f;
    protected float endAttackTimeModifier = 1f;
    protected float damageModifier = 1f;
    protected float reloadTimeModifier = 1f;
    protected bool isReloading = false;

    public float BasePrepareAttackTime => basePrepareAttackTime;
    public float BaseEndAttackTime => baseEndAttackTime;
    public float BaseDamage => baseDamage;
    public float BaseReloadTime => baseReloadTime;
    public int EnergyUsage => energyUsage;
    public float PrepareAttackTimeModifier => prepareAttackTimeModifier;
    public float EndAttackTimeModifier => endAttackTimeModifier;
    public float DamageModifier => damageModifier;
    public float ReloadTimeModifier => reloadTimeModifier;
    public float WeaponDamage => BaseDamage * DamageModifier;

    public void SetBasePrepareAttackTime(float newValue) => basePrepareAttackTime = newValue;
    public void SetBaseEndAttackTime(float newValue) => baseEndAttackTime = newValue;
    public void SetBaseDamage(float newValue) => baseDamage = newValue;
    public void SetBaseReloadTime(float newValue) => baseReloadTime = newValue;
    public void SetEnergyUsage(int newValue) => energyUsage = newValue;
    public void SetPrepareAttackTimeModifier(float newValue) => prepareAttackTimeModifier = newValue;
    public void SetEndAttackTimeModifier(float newValue) => endAttackTimeModifier = newValue;
    public void SetDamageModifier(float newValue) => damageModifier = newValue;
    public void SetReloadTimeModifier(float newValue) => reloadTimeModifier = newValue;

    public virtual void LaunchAttack()
    {
        if (CheckAttackConditions()) {
            isReloading = true;
            StartCoroutine(PerformAttack());
        }
    }

    protected virtual IEnumerator PerformAttack()
    {
        OnPrepareAttackStart();
        yield return new WaitForSeconds(prepareAttackTimeModifier * basePrepareAttackTime);
        OnPrepareAttackEnd();
        Attack();
        OnEndAttackStart();
        yield return new WaitForSeconds(endAttackTimeModifier * baseEndAttackTime);
        OnEndAttackEnd();
        StartCoroutine(ReloadWeapon(reloadTimeModifier * baseReloadTime));
    }

    protected virtual void Attack()
    {

    }

    protected virtual bool CheckAttackConditions()
    {
        return owner.IsEnoughEnergy(energyUsage);
    }

    protected virtual void OnPrepareAttackStart()
    {

    }

    protected virtual void OnPrepareAttackEnd()
    {

    }

    protected virtual void OnEndAttackStart()
    {

    }

    protected virtual void OnEndAttackEnd()
    {

    }
    protected IEnumerator ReloadWeapon(float reloadTime)
    {
        yield return new WaitForSeconds(reloadTime);
        isReloading = false;
    }

    private void Start()
    {
        Init();
    }

    protected virtual void Init()
    {
        WeaponValues weaponValues = WeaponValueManager.Instance.GetWeaponValuesByID(weaponID);

        SetBasePrepareAttackTime(weaponValues.basePrepareAttackTime);
        SetBaseEndAttackTime(weaponValues.baseEndAttackTime);
        SetBaseDamage(weaponValues.baseDamage);
        SetBaseReloadTime(weaponValues.baseReloadTime);
    }
}
