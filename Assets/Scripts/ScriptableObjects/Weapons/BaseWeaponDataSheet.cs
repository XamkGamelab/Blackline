using UnityEngine;

public abstract class BaseWeaponDataSheet : ScriptableObject
{
    [Header("Base Weapon Data")]
    [SerializeField]
    private string _weaponName;
    public string WeaponName => _weaponName;
    [SerializeField]
    private bool _canAkimbo;
    public bool CanAkimbo => _canAkimbo;
}
