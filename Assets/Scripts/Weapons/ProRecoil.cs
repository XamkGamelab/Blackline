using UnityEngine;

public class ProRecoil : MonoBehaviour
{
    [SerializeField]
    private RaycastWeapon _weapon;
    [SerializeField]
    private Transform _recoilBone;

    private Vector3 _currentRotation;
    private Vector3 _targetRotation;

    private Vector3 _currentPosition;
    private Vector3 _targetPosition;

    private Vector3 _recoilZeroPosition;

    private void Awake()
    {
        _recoilZeroPosition = _recoilBone.localPosition;
    }

    private void OnEnable()
    {
        _weapon.OnWeaponFire += HandleOnWeaponFire;
    }

    private void OnDisable()
    {
        _weapon.OnWeaponFire -= HandleOnWeaponFire;
    }

    private void LateUpdate()
    {
        _targetRotation = Vector3.Lerp(_targetRotation, Vector3.zero, _weapon.DataSheet.RecoilReturnSpeed * Time.deltaTime);
        _currentRotation = Vector3.Slerp(_currentRotation, _targetRotation, _weapon.DataSheet.RecoilSnappiness * Time.deltaTime);

        _targetPosition = Vector3.Lerp(_targetPosition, _recoilZeroPosition, _weapon.DataSheet.RecoilReturnSpeed * Time.deltaTime);
        _currentPosition = Vector3.Slerp(_currentPosition, _targetPosition, _weapon.DataSheet.RecoilSnappiness * Time.deltaTime);

        _recoilBone.transform.localRotation = Quaternion.Euler(_targetRotation);
        _recoilBone.transform.localPosition = _currentPosition;
    }

    private void HandleOnWeaponFire()
    {
        float _recoilRotX = _weapon.DataSheet.RecoilRotX;
        float _recoilRotY = _weapon.DataSheet.RecoilRotY;
        float _recoilRotZ = _weapon.DataSheet.RecoilRotZ;

        float _recoilPosX = _weapon.DataSheet.RecoilPosX;
        float _recoilPosY = _weapon.DataSheet.RecoilPosY;
        float _recoilPosZ = _weapon.DataSheet.RecoilPosZ;

        _targetRotation += new Vector3(Random.Range(_recoilRotX / 2f, _recoilRotX), Random.Range(-_recoilRotY, _recoilRotY), Random.Range(-_recoilRotZ, _recoilRotZ));
        _targetPosition += new Vector3(Random.Range(-_recoilPosX, _recoilPosX), Random.Range(_recoilPosY / 2f, _recoilPosY), Random.Range(_recoilPosZ / 2f, _recoilPosZ));
    }
}
