using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class TutorialScreen : BaseScreen
{
    [SerializeField] private ActionButton closeButton;

    public override void Show(bool animate = true)
    {
        base.Show(animate);
        closeButton.Initialize("CLOSE", true, OnCloseButtonClick);
    }

    private void OnCloseButtonClick()
    {
        BaseScreenManager.Instance.SwitchScreen(ScreenType.TutorialScreen, ScreenType.MainMenuScreen);
    }
}