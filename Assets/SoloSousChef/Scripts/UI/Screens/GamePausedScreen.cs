using UnityEngine;

public class GamePausedScreen : BaseScreen
{
    [SerializeField] private ActionButton gameResumeButton;
    [SerializeField] private ActionButton quitGameButton;

    private void Start()
    {
        gameResumeButton.Initialize(ButtonType.Primary, "Resume", true, OnResumeButtonClick);
        quitGameButton.Initialize(ButtonType.Secondary, "Quit", true, OnQuitGameButtonClick);
    }

    private void OnResumeButtonClick()
    {
        SessionManager.Instance.TogglePause(false);
    }
    private void OnQuitGameButtonClick()
    {
        SessionManager.Instance.EndSession();
        OrderManager.Instance.ClearAllOrders();
        BaseScreenManager.Instance.SwitchScreen(ScreenType.GamePausedScreen, ScreenType.MainMenuScreen);
    }
}