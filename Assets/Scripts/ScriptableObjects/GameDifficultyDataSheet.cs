using UnityEngine;

[CreateAssetMenu(fileName = "Game Difficulty Data Sheet", menuName = "Data Sheets/Game Difficulty/Create Game Difficulty Data Sheet")]
public class GameDifficultyDataSheet : ScriptableObject
{
    [Header("Difficulty Info")]
    [SerializeField]
    private string _difficultyName;
    [SerializeField]
    private string _difficultyDescription;

    [Header("Multipliers")]
    [SerializeField]
    [Range(0f, 1f)]
    [Tooltip("How much damage does player armor absorb in percentage.")]
    private float _playerArmorDamageMitigation;

    public string DifficultyName => _difficultyName;
    public string DifficultyDescription => _difficultyDescription;

    public float PlayerArmorDamageMitigation => _playerArmorDamageMitigation;
}
