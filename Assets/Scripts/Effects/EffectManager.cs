using System.Collections.Generic;
using System.Linq;
using UnityEngine;

public class EffectManager : MonoBehaviour
{
    [SerializeField] List<EffectValues> effectValues = new();
    private List<EffectID> effectIDs = new();
    public List<EffectID> EffectIDs => effectIDs;
    public static EffectManager Instance = null;

    private void Awake()
    {
        if (Instance != null)
        {
            Destroy(gameObject);
            return;
        }
        Instance = this;
        DontDestroyOnLoad(this);

        foreach(EffectValues ev in effectValues)
        {
            effectIDs.Add(ev.effectID);
        }
    }
    public EffectValues GetEffectValuesByID(EffectID effectID)
    {
        return effectValues.FirstOrDefault((x) => x.effectID == effectID) ?? null;
    }
    public EffectValues GetEffectValuesByIndex(int index)
    {
        if (index < 0 || index >= effectValues.Count) return null;
        return effectValues[index];
    }
    public int GetEffectTypeCount() => effectValues.Count;
}
