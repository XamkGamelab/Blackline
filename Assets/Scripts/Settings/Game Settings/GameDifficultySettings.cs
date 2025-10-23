using System.Collections.Generic;
using UnityEngine;

[CreateAssetMenu(fileName = "Game Difficulty Holder", menuName = "Data Sheets/Game Difficulty/Create Game Difficulty Holder")]
public class GameDifficultySettings : ScriptableObject
{
    [SerializeField]
    private List<GameDifficultyDataSheet> _difficulties = new List<GameDifficultyDataSheet>();

    public List<GameDifficultyDataSheet> Difficulties => _difficulties;

    // This fetch by name thing is a fucking hack. Fuck it. Fuck this. Fuck all of this. <3 -Shad //
    public GameDifficultyDataSheet GetDifficulty(string name)
    {
        foreach(GameDifficultyDataSheet difficulty in _difficulties)
        {
            if (difficulty.DifficultyName == name) return difficulty;            
        }
        
        return null;
    }
}
