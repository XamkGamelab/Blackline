using UnityEngine;

public abstract class BaseItemPickup : MonoBehaviour
{
    [SerializeField]
    private BaseItemPickupDataSheet _itemDataSheet;
    public BaseItemPickupDataSheet ItemDataSheet => _itemDataSheet;
    [SerializeField]
    private Transform _itemPivot;

    public virtual void OnTriggerEnter(Collider other)
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
        _itemPivot.Rotate(Vector3.up * _itemDataSheet.RotationSpeed * Time.deltaTime);

        _hoverVector = _itemPivot.localPosition;
        _hoverY = Mathf.Sin(_itemDataSheet.WaveSpeed * Time.time) * _itemDataSheet.WaveHeight;

        _hoverVector.y = _itemDataSheet.PivotPointY + _hoverY;
        _itemPivot.localPosition = _hoverVector;
    }
}
