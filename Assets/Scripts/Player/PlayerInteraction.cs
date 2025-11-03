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
        if(_equippedWeapon != null) HandleWeapon(_equippedWeapon);
    }

    private void Start()
    {
        EquipPrimaryWeapon();
    }

    public void EquipPrimaryWeapon()
    {
        if (_playerInventory.PrimaryWeapon == null) return;

        _equippedWeapon = _playerInventory.PrimaryWeapon;
    }

    public void EquipSecondaryWeapon()
    {
        if (_playerInventory.SecondaryWeapon == null) return;

        _equippedWeapon = _playerInventory.SecondaryWeapon;
    }

    public void EquipMeleeWeapon()
    {
        if (_playerInventory.MeleeWeapons.Count == 0) return;
    }

    public void EquipUtilityWeapon()
    {
        if (_playerInventory.UtilityWeapons.Count == 0) return;
    }

    private void HandleWeapon(BaseWeapon weapon)
    {
        weapon.HandleUpdate();
    }
}
