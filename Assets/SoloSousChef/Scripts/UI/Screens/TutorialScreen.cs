using com.SoloSousChef.UI.Components;
using com.SoloSousChef.UI.Managers;
using UnityEngine;
namespace com.SoloSousChef.UI.Screens
{
    public class TutorialScreen : BaseScreen
    {
        [SerializeField] private ActionButton closeButton;

        public override void Show(bool animate = true)
        {
            base.Show(animate);
            closeButton.Initialize(ButtonType.Secondary, "CLOSE", true, OnCloseButtonClick);
        }

        private void OnCloseButtonClick()
        {
            BaseScreenManager.Instance.SwitchScreen(ScreenType.TutorialScreen, ScreenType.MainMenuScreen);
        }
    }
}