using UnityEngine;

public class ItemPickup : MonoBehaviour, IPickup
{
    [SerializeField]
    private Transform _itemPivot;

    [SerializeField]
    private float _rotationSpeed;

    [SerializeField]
    private float _pivotPointY;

    [SerializeField]
    private float _waveHeight;

    [SerializeField]
    private float _waveSpeed;

    public void OnTriggerEnter(Collider other)
    {
        Pickup();
    }

    public void Pickup()
    {

    }

    private void Update()
    {
        ItemHoverEffect();
    }

    private float _hoverY;
    private Vector3 _hoverVector;
    private void ItemHoverEffect()
    {
        _itemPivot.Rotate(Vector3.up * _rotationSpeed * Time.deltaTime);

        _hoverVector = _itemPivot.localPosition;
        _hoverY = Mathf.Sin(_waveSpeed * Time.time) * _waveHeight;

        _hoverVector.y = _pivotPointY + _hoverY;
        _itemPivot.localPosition = _hoverVector;
    }
}
