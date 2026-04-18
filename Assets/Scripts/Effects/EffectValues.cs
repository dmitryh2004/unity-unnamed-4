using System.Collections.Generic;
using System.Linq;
using UnityEngine;

public enum EffectID
{
    Dash = 1
}

[System.Serializable]
public class EffectValue
{
    public string key;
    public float value;
}

[CreateAssetMenu(fileName = "Effect values", menuName = "Scriptable Objects/Effect Values")]
public class EffectValues : ScriptableObject
{
    public EffectID effectID;
    public int maxCount = 1;
    public float duration = 1f; // -1 for infinite effects
    public List<EffectValue> effectValues = new();

    public float? GetEffectValue(string key)
    {
        return effectValues.FirstOrDefault((x) => (x.key == key))?.value;
    }
}
