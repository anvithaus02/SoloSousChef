
using com.SoloSousChef.Manager;
using com.SoloSousChef.Order;
using com.SoloSousChef.UI.Components;
using com.SoloSousChef.UI.Managers;
using TMPro;
using UnityEngine;
namespace com.SoloSousChef.UI.Screens
{
    public class TimeCompletedScreen : BaseScreen
    {
        [SerializeField] private TextMeshProUGUI _scoreInfoText;
        [SerializeField] private ActionButton _quitButton;

        private void Start()
        {
            Initialze();
        }
        public void Initialze()
        {
            int highScore = ScoreManager.Instance.GetHighScore();
            int score = ScoreManager.Instance.GetCurrentScore();
            _scoreInfoText.text = $"Best Score : {highScore}\n" + $"Current Score : {score}";

            _quitButton.Initialize(ButtonType.Secondary, "Quit", true, OnQuitGameButtonClick);
        }

        private void OnQuitGameButtonClick()
        {
            SessionManager.Instance.EndSession();
            OrderManager.Instance.ClearAllOrders();
            BaseScreenManager.Instance.SwitchScreen(ScreenType.TimeCompletedScreen, ScreenType.MainMenuScreen);
        }
    }
}