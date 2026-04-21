public class QuitGameButton : ActionButton
{
    public void Initialize()
    {
        base.Initialize(ButtonType.Secondary,"Quit", true, OnQuitGameButtonClick);
    }

    private void OnQuitGameButtonClick()
    {
        SessionManager.Instance.EndSession();
        OrderManager.Instance.ClearAllOrders();
        BaseScreenManager.Instance.SwitchScreen(ScreenType.GamePausedScreen, ScreenType.MainMenuScreen);
    }
}