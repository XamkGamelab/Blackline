using UnityEngine;

public abstract class BaseWeaponDataSheet : ScriptableObject
{
    [Header("Base Weapon Data")]
    [SerializeField]
    private WeaponCategory _weaponCategory;
    public WeaponCategory WeaponCategory => _weaponCategory;
    [SerializeField]
    private string _weaponName;
    public string WeaponName => _weaponName;
    [SerializeField]
    private float _drawTime;
    public float DrawTime => _drawTime;
    [SerializeField]
    private float _holsterTime;
    public float HolsterTime => _holsterTime;
    [SerializeField]
    private bool _canAkimbo;
    public bool CanAkimbo => _canAkimbo;

    [Header("Sway")]
    [SerializeField]
    private float _swayAmountRot;
    public float SwayAmountRot => _swayAmountRot;
    [SerializeField]
    private float _swaySmoothnessRot;
    public float SwaySmoothnessRot => _swaySmoothnessRot;
    [SerializeField]
    private float _swayAmountPos;
    public float SwayAmountPos => _swayAmountPos;
    [SerializeField]
    private float _swaySmoothnessPos;
    public float SwaySmoothnessPos => _swaySmoothnessPos;

    [Header("Animation")]
    [SerializeField]
    private string _weaponKeyword;
    public string WeaponKeyword => _weaponKeyword;
    [SerializeField]
    private Quaternion _leftHandIKRotationOffset;
    public Quaternion LeftHandIKRotationOffset => _leftHandIKRotationOffset;
    [SerializeField]
    private Quaternion _rightHandIKRotationOffset;
    public Quaternion RightHandIKRotationOffset => _rightHandIKRotationOffset;
}

public enum WeaponCategory
{
    Melee,
    Light,
    Shell,
    Medium,
    Heavy,
    Plasma,
    Rocket,
    Utility,
    Throwable,
}
