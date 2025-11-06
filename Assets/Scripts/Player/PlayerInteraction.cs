using UnityEngine;

public class PlayerInteraction : MonoBehaviour
{
    [SerializeField]
    private PlayerInventory _playerInventory;

    private void Update()
    {
        // This is bad because weapon might be null. But fuck it for now. -Shad //
        if(_playerInventory.EquippedWeapon != null) HandleWeapon(_playerInventory.EquippedWeapon);

        if (Input.GetKeyDown(GlobalSettingsHolder.Instance.PlayerSettingsData.MeleeKey)) _playerInventory.SelectWeaponByKey(GlobalSettingsHolder.Instance.PlayerSettingsData.MeleeKey);
        if (Input.GetKeyDown(GlobalSettingsHolder.Instance.PlayerSettingsData.LightCategoryKey)) _playerInventory.SelectWeaponByKey(GlobalSettingsHolder.Instance.PlayerSettingsData.LightCategoryKey);
        if (Input.GetKeyDown(GlobalSettingsHolder.Instance.PlayerSettingsData.ShellCategoryKey)) _playerInventory.SelectWeaponByKey(GlobalSettingsHolder.Instance.PlayerSettingsData.ShellCategoryKey);
        if (Input.GetKeyDown(GlobalSettingsHolder.Instance.PlayerSettingsData.MediumCategoryKey)) _playerInventory.SelectWeaponByKey(GlobalSettingsHolder.Instance.PlayerSettingsData.MediumCategoryKey);
        if (Input.GetKeyDown(GlobalSettingsHolder.Instance.PlayerSettingsData.HeavyCategoryKey)) _playerInventory.SelectWeaponByKey(GlobalSettingsHolder.Instance.PlayerSettingsData.HeavyCategoryKey);
        if (Input.GetKeyDown(GlobalSettingsHolder.Instance.PlayerSettingsData.PlasmaCategoryKey)) _playerInventory.SelectWeaponByKey(GlobalSettingsHolder.Instance.PlayerSettingsData.PlasmaCategoryKey);
        if (Input.GetKeyDown(GlobalSettingsHolder.Instance.PlayerSettingsData.RocketCategoryKey)) _playerInventory.SelectWeaponByKey(GlobalSettingsHolder.Instance.PlayerSettingsData.RocketCategoryKey);
        if (Input.GetKeyDown(GlobalSettingsHolder.Instance.PlayerSettingsData.UtilityCategoryKey)) _playerInventory.SelectWeaponByKey(GlobalSettingsHolder.Instance.PlayerSettingsData.UtilityCategoryKey);
        if (Input.GetKeyDown(GlobalSettingsHolder.Instance.PlayerSettingsData.ThrowableCategoryKey)) _playerInventory.SelectWeaponByKey(GlobalSettingsHolder.Instance.PlayerSettingsData.ThrowableCategoryKey);
    }

    private void HandleWeapon(BaseWeapon weapon)
    {
        weapon.HandleFunctions();
    }
}
