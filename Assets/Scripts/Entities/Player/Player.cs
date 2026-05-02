using UnityEngine;
using UnityEngine.InputSystem;

public class Player : Entity
{
    public override void AddEffect(EffectID effectID, int count = 1)
    {
        base.AddEffect(effectID, count);
        if (effectID == EffectID.Dash)
        {
            UpdateDashAnimation();
        }
    }
    public override void RemoveEffect(EffectID effectID, int count = 1)
    {
        base.RemoveEffect(effectID, count);
        if (effectID == EffectID.Dash)
        {
            UpdateDashAnimation();
        }
    }
    public override void RemoveAllEffectOfType(EffectID effectID)
    {
        base.RemoveAllEffectOfType(effectID);
        if (effectID == EffectID.Dash)
        {
            UpdateDashAnimation();
        }
    }

    public override void RemoveAllEffects()
    {
        base.RemoveAllEffects();
        UpdateDashAnimation();
    }
    void UpdateDashAnimation()
    {
        animator.SetBool("dashing", HasEffect(EffectID.Dash));
    }

    protected override void ChangeWeaponPosition()
    {
        if (weaponObject == null) return;
        if (InputManager.Instance.MouseAim)
        {
            Vector2 screenCenter = new Vector2(Screen.width * 0.5f, Screen.height * 0.5f);
            Vector2 mousePos = Mouse.current.position.ReadValue();
            Vector2 dirFromCenter = mousePos - screenCenter;

            Vector2 normalizedDirection = dirFromCenter.normalized;
            if (normalizedDirection == Vector2.zero)
            {
                normalizedDirection = Vector2.right;
            }
            Vector2 weaponPosition = normalizedDirection * weaponOffsetDistance;
            float angle = Vector2.SignedAngle(Vector2.right, normalizedDirection);
            weaponObject.localPosition = weaponPosition;
            weaponObject.localRotation = Quaternion.Euler(0f, 0f, angle);
            float scale = (Mathf.Cos(Mathf.Deg2Rad * angle) >= 0) ? 1 : -1;
            weaponObject.localScale = new Vector2(1f, scale);
        }
        else
            base.ChangeWeaponPosition();
    }

    protected override void OnDeath()
    {
        Debug.Log("You died!");
    }
}
