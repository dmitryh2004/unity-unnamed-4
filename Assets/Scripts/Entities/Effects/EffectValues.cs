using System.Collections.Generic;
using UnityEngine;

public enum EffectID
{
    Burning = 0
}

[System.Serializable]
public class EffectValue
{
    public string key;
    public float value;
}

[CreateAssetMenu(fileName = "EffectValues", menuName = "Scriptable Objects/EffectValues")]
public class EffectValues : ScriptableObject
{
    public EffectID effectID;
    public List<EffectValue> effectValues = new();
}
