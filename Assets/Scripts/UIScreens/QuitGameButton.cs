public class QuitGameButton : ActionButton
{
    public void Initialize()
    {
        base.Initialize("QUIT", true, OnQuitGameButtonClick);
    }

    private void OnQuitGameButtonClick()
    {
        SessionManager.Instance.EndSession();
        BaseScreenManager.Instance.SwitchScreen(ScreenType.GamePausedScreen, ScreenType.MainMenuScreen);
    }
}