using UnityEngine;

public abstract class BaseAmmoDataSheet : ScriptableObject
{
    [SerializeField]
    private string _ammoName;
    [SerializeField]
    private float _damage;
    [SerializeField]
    [Range(0, 1f)]
    private float _armorPenetration;
    [SerializeField]
    private float _projectileSpeed;
    [SerializeField]
    private DamageType _damageType;

    [HideInInspector]
    public GameObject Source;

    public string AmmoName => _ammoName;
    public float Damage => _damage;
    public float ArmorPenetration => _armorPenetration;
    public float ProjectileSpeed => _projectileSpeed;
    public DamageType DamageType => _damageType;

    public abstract void OnHit(RaycastHit hit, GameObject source);
}
