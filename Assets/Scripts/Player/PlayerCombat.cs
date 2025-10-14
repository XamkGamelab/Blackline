using UnityEngine;
using Zenject;

public class PlayerCombat : MonoBehaviour
{
    [SerializeField]
    private PlayerInventory _playerInventory;

    // The current weapon the player is holding. -Davoth //
    private BaseWeapon _equippedWeapon;

    private void Update()
    {
        // This is bad because slot might be null. But fuck it for now. -Davoth //
        HandleWeapon(_playerInventory.SecondaryWeapon);
    }

    private void HandleWeapon(BaseWeapon weapon)
    {
        if (Input.GetKeyDown(SettingsHolder.Data.ShootKey))
        {
            weapon.PrimaryFunction();
        }

        if (Input.GetKeyDown(SettingsHolder.Data.AimKey))
        {
            weapon.SecondaryFunction();
        }
    }
}
