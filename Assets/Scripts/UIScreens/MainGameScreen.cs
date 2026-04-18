using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class MainGameScreen : BaseScreen
{
    [SerializeField] private QuitGameButton quitGameButton;
    [SerializeField] private ActionButton tutorialButton;
    [SerializeField] private ActionButton startGameButton;

    private void Start()
    {
        startGameButton.Initialize("START", true, OnStartGameButtonClick);
        tutorialButton.Initialize("TUTORIAL", true, OnTutorialButtonClick);
        quitGameButton.Initialize();
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
