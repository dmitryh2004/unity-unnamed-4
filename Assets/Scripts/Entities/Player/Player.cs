using UnityEngine;

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
    protected override void OnDeath()
    {
        Debug.Log("You died!");
    }
}
