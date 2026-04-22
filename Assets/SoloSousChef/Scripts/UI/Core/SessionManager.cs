using UnityEngine;
using System;
using com.SoloSousChef.Player;
using com.SoloSousChef.UI.Generic;
using com.SoloSousChef.UI.Screens;
namespace com.SoloSousChef.UI.Managers
{
    public class SessionManager : MonoBehaviour
    {
        public static SessionManager Instance { get; private set; }
        public event Action<int> OnTimerUpdated;
        public event Action OnSessionStarted;
        public event Action OnSessionEnded;
        public event Action<bool> OnPauseToggled;
        private TickTimer _sessionTimer;
        public bool IsSessionActive { get; private set; }
        public bool IsPaused { get; private set; }
        private const int sessionDuration = 180;

        private void Awake()
        {
            if (Instance != null && Instance != this) { Destroy(gameObject); return; }
            Instance = this;
        }

        public void StartSession()
        {
            IsSessionActive = true;
            IsPaused = false;

            OnSessionStarted?.Invoke();
            PlayerController.Instance.SetPlayerDisplayState(true);
            _sessionTimer = new TickTimer(this, sessionDuration, true,
                (time) => OnTimerUpdated?.Invoke(Mathf.CeilToInt(time)),
                EndGameOnTimeComplete
            );
        }

        public int GetInitialDuration()
        {
            return sessionDuration;
        }

        public void TogglePause(bool pause)
        {
            if (!IsSessionActive) return;

            IsPaused = pause;
            OnPauseToggled?.Invoke(IsPaused);

            if (IsPaused)
            {
                _sessionTimer.IsPaused = true;
                BaseScreenManager.Instance.ShowScreen(ScreenType.GamePausedScreen);
            }
            else
            {
                _sessionTimer.Resume();
                BaseScreenManager.Instance.HideScreen(ScreenType.GamePausedScreen);
            }
        }

        public void EndSession()
        {
            IsSessionActive = false;
            _sessionTimer?.Stop(false);
            OnSessionEnded?.Invoke();
            PlayerController.Instance.SetPlayerDisplayState(false);
        }

        public void EndGameOnTimeComplete()
        {
            BaseScreenManager.Instance.ShowScreen(ScreenType.TimeCompletedScreen);
            EndSession();
        }

        public void Cleanup()
        {
            IsSessionActive = false;
            IsPaused = false;
            _sessionTimer?.Stop(false);
        }
    }
}