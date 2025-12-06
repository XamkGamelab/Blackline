using UnityEngine;

public abstract class BaseEnemyDataSheet : ScriptableObject
{
    [Header("Base Enemy Data")]
    [SerializeField]
    private string _manufacturer;
    public string Manufacturer => _manufacturer;
    [SerializeField]
    private string _enemyName;
    public string EnemyName => _enemyName;
}
