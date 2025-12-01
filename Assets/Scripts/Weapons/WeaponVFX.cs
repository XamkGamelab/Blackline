using UnityEngine;

public class WeaponVFX : MonoBehaviour
{
    [SerializeField]
    private BaseWeapon _weapon;

    [SerializeField]
    private ParticleSystem[] _primaryFunctionFX;

    private void OnEnable()
    {
        _weapon.WeaponPrimaryEvent += OnWeaponPrimaryFunction;
    }

    private void OnDisable()
    {
        _weapon.WeaponPrimaryEvent -= OnWeaponPrimaryFunction;
    }

    private void OnWeaponPrimaryFunction()
    {
        foreach (var FX in _primaryFunctionFX) FX.Play();
    }
}
