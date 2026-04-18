using System.Collections.Generic;
using System.Linq;
using UnityEngine;

public class WeaponValueManager : MonoBehaviour
{
    [SerializeField] List<WeaponValues> weaponValues = new ();

    public static WeaponValueManager Instance = null;

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

    public WeaponValues GetEntityValuesByID(WeaponID weaponID)
    {
        return weaponValues.FirstOrDefault((x) => x.weaponID == weaponID) ?? null;
    }
}
