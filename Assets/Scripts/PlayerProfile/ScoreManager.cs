using UnityEngine;
using System;

public class ScoreManager
{
    // Standard C# Singleton pattern
    private static ScoreManager _instance;
    public static ScoreManager Instance => _instance ??= new ScoreManager();

    // Events for UI and VFX to listen to
    public event Action<int> OnScoreChanged;
    public event Action<int, Vector3> OnScoreAddedWithPosition;

    private int _currentScore = 0;
    private int _highScore = 0;
    private const string HIGH_SCORE_KEY = "HighScore";

    // Constructor - Logic moved here from Awake
    private ScoreManager()
    {
        _highScore = PlayerPrefs.GetInt(HIGH_SCORE_KEY, 0);
    }

    public void AddScore(int amount, Vector3 worldPosition)
    {
        _currentScore += amount;
        
        if (_currentScore > _highScore)
        {
            _highScore = _currentScore;
            PlayerPrefs.SetInt(HIGH_SCORE_KEY, _highScore);
            PlayerPrefs.Save();
        }

        // Notify UI/VFX observers
        OnScoreChanged?.Invoke(_currentScore);
        OnScoreAddedWithPosition?.Invoke(amount, worldPosition);
    }

    public void ResetScore()
    {
        _currentScore = 0;
        OnScoreChanged?.Invoke(_currentScore);
    }

    public int GetCurrentScore() => _currentScore;
    public int GetHighScore() => _highScore;
}