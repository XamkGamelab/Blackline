using UnityEngine;

public class PlayerInteraction : MonoBehaviour
{
    [SerializeField]
    private PlayerInventory _playerInventory;

    private void Update()
    {
        // This is bad because weapon might be null. But fuck it for now. -Shad //
        if(_playerInventory.EquippedWeapon != null) HandleWeapon(_playerInventory.EquippedWeapon);
    }

    private void HandleWeapon(BaseWeapon weapon)
    {
        weapon.HandleUpdate();
    }

    public void SelectWeaponByKey(KeyCode keyCode)
    {
        //if (index < 0 || index >= _playerInventory.WeaponSlots.Count) return;

        //_playerInventory.SelectWeaponByNumber(index);
    }
}
