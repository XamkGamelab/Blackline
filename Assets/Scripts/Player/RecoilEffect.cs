using UnityEngine;

public class RecoilEffect : MonoBehaviour
{
    [SerializeField]
    private PlayerMovement _playerMovement;
    [SerializeField]
    private PlayerLook _playerLook;
    [SerializeField]
    private PlayerInventory _playerInventory;

    private Vector3 _currentRotation, _targetRotation;

    private void Start()
    {
        _playerInventory.EquippedWeapon.WeaponPrimaryEvent += RecoilCam;
    }

    private void OnDestroy()
    {
        _playerInventory.EquippedWeapon.WeaponPrimaryEvent -= RecoilCam;
    }

    private void Update()
    {
        _targetRotation = Vector3.Lerp(_targetRotation, Vector3.zero, _playerInventory.EquippedWeapon.WeaponData.PrimaryFunctionSmoothness * Time.deltaTime);
        _currentRotation = Vector3.Slerp(_currentRotation, _targetRotation, _playerInventory.EquippedWeapon.WeaponData.PrimaryFunctionRecoil * Time.deltaTime);

        transform.localRotation = Quaternion.Euler(_currentRotation);        
    }

    public void RecoilCam()
    {
        if(_playerInventory.EquippedWeapon is RaycastWeapon raycastWeapon)
        {
            RaycastWeaponDataSheet raycastWeaponData = raycastWeapon.DataSheet;

            _targetRotation += new Vector3(Random.Range(raycastWeaponData.RecoilRotX / 2, raycastWeaponData.RecoilRotX), Random.Range(-raycastWeaponData.RecoilRotY, raycastWeaponData.RecoilRotY), Random.Range(-raycastWeaponData.RecoilRotZ, raycastWeaponData.RecoilRotZ));
        }        
    }
}
