using UnityEngine;

public class PlayerInteraction : MonoBehaviour
{
    [SerializeField]
    private PlayerInventory _playerInventory;

    // The current weapon the player is holding. -Shad //
    private BaseWeapon _equippedWeapon;

    private void Update()
    {
        // This is bad because weapon might be null. But fuck it for now. -Shad //
        HandleWeapon(_playerInventory.SecondaryWeapon);
    }

    private void HandleWeapon(BaseWeapon weapon)
    {
        if (Input.GetKeyDown(GlobalSettingsHolder.Instance.PlayerSettingsData.ShootKey))
        {
            weapon.PrimaryFunction();
        }

        if (Input.GetKeyDown(GlobalSettingsHolder.Instance.PlayerSettingsData.AimKey))
        {
            weapon.SecondaryFunction();
        }
    }
}
