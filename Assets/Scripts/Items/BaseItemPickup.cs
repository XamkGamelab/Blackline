using UnityEngine;

public abstract class BaseItemPickup : MonoBehaviour, IPickup
{
    [SerializeField]
    private BaseItemPickupDataSheet _dataSheet;
    public BaseItemPickupDataSheet DataSheet => _dataSheet;
    [SerializeField]
    private Transform _itemPivot;

    public void OnTriggerEnter(Collider other)
    {
        Pickup();
    }

    private void Update()
    {
        ItemHoverEffect();
    }

    public virtual void Pickup()
    {
        print("Trigger entered!");
    }

    private float _hoverY;
    private Vector3 _hoverVector;
    private void ItemHoverEffect()
    {
        _itemPivot.Rotate(Vector3.up * _dataSheet.RotationSpeed * Time.deltaTime);

        _hoverVector = _itemPivot.localPosition;
        _hoverY = Mathf.Sin(_dataSheet.WaveSpeed * Time.time) * _dataSheet.WaveHeight;

        _hoverVector.y = _dataSheet.PivotPointY + _hoverY;
        _itemPivot.localPosition = _hoverVector;
    }
}
