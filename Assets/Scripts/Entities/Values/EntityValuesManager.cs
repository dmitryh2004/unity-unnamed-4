using System.Collections.Generic;
using System.Linq;
using UnityEngine;

public class EntityValuesManager : MonoBehaviour
{
    [SerializeField] List<EntityValues> entityValues = new();

    public static EntityValuesManager Instance = null;

    private void Awake()
    {
        if (Instance != null)
        {
            Destroy(gameObject);
            return;
        }
        Instance = this;
        DontDestroyOnLoad(this);
    }

    public EntityValues GetEntityValuesByID(EntityID entityID)
    {
        return entityValues.FirstOrDefault((x) => x.entityID == entityID) ?? null;
    }
}
