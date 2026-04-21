using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class MainGameScreen : BaseScreen
{
    [SerializeField] private ActionButton tutorialButton;
    [SerializeField] private ActionButton startGameButton;

    private void Start()
    {
        startGameButton.Initialize(ButtonType.Primary,"Start", true, OnStartGameButtonClick);
        tutorialButton.Initialize(ButtonType.Secondary,"Tutorial", true, OnTutorialButtonClick);
    }

    private void OnStartGameButtonClick()
    {
        SessionManager.Instance.StartSession();
        BaseScreenManager.Instance.SwitchScreen(ScreenType.MainMenuScreen, ScreenType.GamePlayScreen);
    }

    private void OnTutorialButtonClick()
    {
        BaseScreenManager.Instance.SwitchScreen(ScreenType.MainMenuScreen, ScreenType.TutorialScreen);
    }
}
