using UnityEngine;

public class GamePausedScreen : BaseScreen
{
    [SerializeField] private ActionButton gameResumeButton;
    [SerializeField] private QuitGameButton quitGameButton;

    private void Start()
    {
        gameResumeButton.Initialize(ButtonType.Primary,"Resume", true, OnResumeButtonClick);
        quitGameButton.Initialize();
    }

    private void OnResumeButtonClick()
    {
        SessionManager.Instance.TogglePause(false);
    }
}