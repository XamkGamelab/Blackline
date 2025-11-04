using UnityEngine;

public class BaseItemPickupDataSheet : ScriptableObject
{
    [Header("Hovering Effect")]
    [SerializeField]
    private float _rotationSpeed;
    public float RotationSpeed => _rotationSpeed;
    [SerializeField]
    private float _pivotPointY;
    public float PivotPointY => _pivotPointY;
    [SerializeField]
    private float _waveHeight;
    public float WaveHeight => _waveHeight;
    [SerializeField]
    private float _waveSpeed;
    public float WaveSpeed => _waveSpeed;
}
