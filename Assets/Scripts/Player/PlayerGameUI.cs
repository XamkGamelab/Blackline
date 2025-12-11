using TMPro;
using UnityEngine;
using UnityEngine.UI;
using Zenject;

public class PlayerGameUI : MonoBehaviour
{
    [Header("Player References")]
    [SerializeField]
    private PlayerInventory _playerInventory;
    [SerializeField]
    private PlayerHealth _playerHealth;

    [Space]

    [Header("UI References")]
    [SerializeField]
    private TextMeshProUGUI _healthText;
    [SerializeField]
    private Slider _healthSlider;
    [SerializeField]
    private TextMeshProUGUI _armorText;
    [SerializeField]
    private Slider _armorSlider;

    [Space]

    [SerializeField]
    private TextMeshProUGUI _weaponName;
    [SerializeField]
    private TextMeshProUGUI _currentAmmo;
    [SerializeField]
    private TextMeshProUGUI _inventoryAmmo;

    private PlayerDataSheet PlayerData;

    [Inject]
    private void Construct(PlayerDataSheet playerData) => PlayerData = playerData;

    public void Start()
    {
        _playerInventory.WeaponEquipEvent += OnWeaponEquipped;

        _playerHealth.DamageTakenEvent += OnDamageTaken;
    }

    public void OnDestroy()
    {
        _playerInventory.WeaponEquipEvent -= OnWeaponEquipped;
        _playerInventory.EquippedWeapon.WeaponPrimaryEvent -= OnWeaponPrimary;
        _playerInventory.EquippedWeapon.WeaponReloadedEvent -= OnWeaponReloaded;

        _playerHealth.DamageTakenEvent -= OnDamageTaken;
    }

    #region Weapon Methods
    private void OnWeaponEquipped()
    {
        foreach (BaseWeapon key in _playerInventory.OwnedWeapons.Keys)
        {
            key.WeaponPrimaryEvent -= OnWeaponPrimary;
            key.WeaponReloadedEvent -= OnWeaponReloaded;
        }

        _playerInventory.EquippedWeapon.WeaponPrimaryEvent += OnWeaponPrimary;
        _playerInventory.EquippedWeapon.WeaponReloadedEvent += OnWeaponReloaded;
    }

    private void OnWeaponPrimary()
    {
        if(_playerInventory.EquippedWeapon is RaycastWeapon raycastWeapon)
        {
            _currentAmmo.text = raycastWeapon.LoadedAmmoCount.ToString();
        }
    }

    private void OnWeaponReloaded()
    {
        if (_playerInventory.EquippedWeapon is RaycastWeapon raycastWeapon)
        {
            _currentAmmo.text = raycastWeapon.LoadedAmmoCount.ToString();
        }
    }
    #endregion

    #region Health Methods    
    private void OnDamageTaken()
    {
        _healthText.text = Mathf.RoundToInt(_playerHealth.CurrentHealth).ToString();
        _healthSlider.value = _playerHealth.CurrentHealth / PlayerData.MaxHealth;

        _armorText.text = Mathf.RoundToInt(_playerHealth.CurrentArmor).ToString();
        _armorSlider.value = _playerHealth.CurrentArmor / PlayerData.MaxArmor;
    }
    #endregion
}
