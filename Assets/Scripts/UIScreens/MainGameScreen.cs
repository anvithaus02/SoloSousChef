using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class MainGameScreen : BaseScreen
{
    [SerializeField] private QuitGameButton quitGameButton;
    [SerializeField] private ActionButton startGameButton;

    private void Start()
    {
        startGameButton.Initialize("START ", true, OnStartGameButtonClick);
        quitGameButton.Initialize();
    }

    private void OnStartGameButtonClick()
    {
        SessionManager.Instance.StartSession();
        BaseScreenManager.Instance.SwitchScreen(ScreenType.MainMenuScreen, ScreenType.GamePlayScreen);
    }
}
