using UnityEngine;
using TMPro;
using System;
using DG.Tweening;

public class GamePlayHUD : MonoBehaviour
{
    [Header("Score UI")]
    [SerializeField] private TextMeshProUGUI currentScoreText;
    [SerializeField] private TextMeshProUGUI bestScoreText;

    [Header("Session UI")]
    [SerializeField] private TextMeshProUGUI gameTimerText;
    [SerializeField] private ActionButton pauseGameButton;
    [SerializeField] private ActionButton quitGameButton;

    [Header("VFX")]
    [SerializeField] private GameObject floatingScorePrefab;

    private void OnEnable()
    {
        ScoreManager.Instance.OnScoreChanged += UpdateScoreUI;
        ScoreManager.Instance.OnScoreAddedWithPosition += SpawnFloatingText;

        if (SessionManager.Instance != null)
        {
            SessionManager.Instance.OnTimerUpdated += UpdateTimerUI;
        }
    }

    private void OnDisable()
    {
        ScoreManager.Instance.OnScoreChanged -= UpdateScoreUI;
        ScoreManager.Instance.OnScoreAddedWithPosition -= SpawnFloatingText;


        if (SessionManager.Instance != null)
        {
            SessionManager.Instance.OnTimerUpdated -= UpdateTimerUI;
        }
    }

    public void Initialize()
    {
        UpdateScoreUI(ScoreManager.Instance.GetCurrentScore());
        bestScoreText.text = $"Best: {ScoreManager.Instance.GetHighScore()}";

        pauseGameButton.Initialize(ButtonType.Pause, string.Empty, true, () =>
        {
            SessionManager.Instance.TogglePause(true);
        });

        quitGameButton.Initialize(ButtonType.Quit, string.Empty, true, OnQuitGameButtonClick);
    }
    private void UpdateScoreUI(int score)
    {
        currentScoreText.text = $"Score: {score}";
        currentScoreText.transform.DOPunchScale(Vector3.one * 0.1f, 0.2f);
    }

    private void UpdateTimerUI(int secondsRemaining)
    {
        TimeSpan time = TimeSpan.FromSeconds(secondsRemaining);
        gameTimerText.text = $"Time : {time.Minutes:D2}m {time.Seconds:D2}s";
    }

    private void SpawnFloatingText(int amount, Vector3 worldPosition)
    {
        if (floatingScorePrefab == null) return;

        GameObject go = Instantiate(floatingScorePrefab, worldPosition, Quaternion.identity);
        var textComp = go.GetComponentInChildren<TextMeshPro>();

        if (textComp != null)
        {
            textComp.text = (amount >= 0 ? "+" : "") + amount;
            textComp.color = amount >= 0 ? Color.green : Color.red;

            go.transform.DOMoveY(worldPosition.y + 1.5f, 1f);
            textComp.DOFade(0, 1f).OnComplete(() => Destroy(go));
        }
    }

    private void OnQuitGameButtonClick()
    {
        SessionManager.Instance.EndSession();
        OrderManager.Instance.ClearAllOrders();
        BaseScreenManager.Instance.SwitchScreen(ScreenType.GamePausedScreen, ScreenType.MainMenuScreen);
    }
}