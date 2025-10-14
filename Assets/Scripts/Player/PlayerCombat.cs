using UnityEngine;
using Zenject;

public class PlayerCombat : MonoBehaviour
{
    [SerializeField]
    private PlayerInventory _playerInventory;

    private SettingsHolder _settings;

    // The current weapon the player is holding. -Davoth //
    private BaseWeapon _equippedWeapon;

    [Inject]
    private void Construct(SettingsHolder settings)
    {
        _settings = settings;
    }

    private void Update()
    {
        // This is bad because Primary Weapon slot might be null. But fuck it for now. -Davoth //
        HandleWeapon(_playerInventory.SecondaryWeapon);
    }

    private void HandleWeapon(BaseWeapon weapon)
    {
        if (Input.GetKeyDown(_settings.Data.ShootKey))
        {
            weapon.PrimaryFunction();
        }

        if (Input.GetKeyDown(_settings.Data.AimKey))
        {
            weapon.SecondaryFunction();
        }
    }
}
