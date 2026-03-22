using UnityEngine;

public interface Effectable
{
    public void AddEffect(EffectID effectID, int count = 1);
    public void RemoveEffect(EffectID effectID, int count = 1);
    public bool HasEffect(EffectID effectID);
    public int GetEffectCount(EffectID effectID);
    public float GetEffectRemainingTime(EffectID effectID);
}
