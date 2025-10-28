using UnityEngine;

public abstract class BaseAmmoDataSheet : ScriptableObject
{
    [SerializeField]
    private int _projectilesPerShot;
    [SerializeField]
    private string _ammoName;
    [SerializeField]
    private float _damage;
    [SerializeField]
    [Range(0f, 1f)]
    private float _armorPenetration;
    [Range(0f, 1f)]
    [SerializeField]
    private float _projectileRange;
    [SerializeField]
    private float _projectileSpeed;
    [SerializeField]
    private DamageType _damageType;

    [HideInInspector]
    public GameObject Source;

    public int ProjectilesPerShot;
    public string AmmoName => _ammoName;
    public float Damage => _damage;
    public float ArmorPenetration => _armorPenetration;
    public float ProjectileRange => _projectileRange;
    public float ProjectileSpeed => _projectileSpeed;
    public DamageType DamageType => _damageType;

    public abstract void OnHit(RaycastHit hit, GameObject source);
}
