using TMPro;
using UnityEngine;
using UnityEngine.UI;

public class ScoreManager : MonoBehaviour
{
    public static ScoreManager Instance { get; private set; }

    [Header("UI References")]
    [SerializeField] private TextMeshProUGUI currentScoreText;
    [SerializeField] private TextMeshProUGUI highScoreText;
    [SerializeField] private GameObject floatingScorePrefab; // Simple prefab with a Text component and a "FloatUp" animation

    private int _currentScore = 0;
    private int _highScore = 0;

    private const string HIGH_SCORE_KEY = "HighScore";

    private void Awake()
    {
        if (Instance == null) Instance = this;
        else Destroy(gameObject);

        LoadHighScore();
    }

    private void Start()
    {
        UpdateUI();
    }

    public void AddScore(int amount, Vector3 worldPosition)
    {
        _currentScore += amount;
        
        // Handle High Score logic
        if (_currentScore > _highScore)
        {
            _highScore = _currentScore;
            SaveHighScore();
        }

        UpdateUI();
        ShowFloatingText(amount, worldPosition);
    }

    private void UpdateUI()
    {
        currentScoreText.text = $"Score: {_currentScore}";
        highScoreText.text = $"High Score: {_highScore}";
    }

    private void ShowFloatingText(int amount, Vector3 worldPosition)
    {
        if (floatingScorePrefab == null) return;

        // Instantiate at the window/table position
        GameObject go = Instantiate(floatingScorePrefab, worldPosition, Quaternion.identity);
        Text t = go.GetComponentInChildren<Text>();
        
        if (t != null)
        {
            t.text = (amount >= 0 ? "+" : "") + amount.ToString();
            t.color = amount >= 0 ? Color.green : Color.red;
        }

        // The prefab should have a self-destruct script or animation
        Destroy(go, 2f);
    }

    private void LoadHighScore()
    {
        _highScore = PlayerPrefs.GetInt(HIGH_SCORE_KEY, 0);
    }

    private void SaveHighScore()
    {
        PlayerPrefs.SetInt(HIGH_SCORE_KEY, _highScore);
        PlayerPrefs.Save();
    }

    public int GetCurrentScore() => _currentScore;
}