using System;
using System.Collections;
using UnityEngine;
namespace com.SoloSousChef.UI.Generic
{
    public class TickTimer
    {
        public int CurrentTime { get; private set; }
        public bool IsRunning { get; private set; }

        private int _duration;
        private bool _isCountDown;
        private Action<int> _onTick;
        private Action _onComplete;
        private Coroutine _activeCoroutine;
        private MonoBehaviour _runner;

        public bool IsPaused { get; set; }
        public void Resume() => IsPaused = false;
        public TickTimer(MonoBehaviour runner, int duration, bool isCountDown, Action<int> onTick, Action onComplete = null)
        {
            _runner = runner;
            _duration = duration;
            _isCountDown = isCountDown;
            _onTick = onTick;
            _onComplete = onComplete;

            Reset();
            _activeCoroutine = _runner.StartCoroutine(TimerRoutine());
        }

        public void Stop(bool triggerComplete = true)
        {
            if (!IsRunning) return;

            IsRunning = false;
            if (_activeCoroutine != null) _runner.StopCoroutine(_activeCoroutine);

            if (triggerComplete)
            {
                _onComplete?.Invoke();
            }
        }

        public void Reset()
        {
            CurrentTime = _isCountDown ? _duration : 0;
        }

        private IEnumerator TimerRoutine()
        {
            IsRunning = true;
            while (IsRunning)
            {
                while (IsPaused)
                {
                    yield return null;
                }
                _onTick?.Invoke(CurrentTime);

                if (_isCountDown && CurrentTime <= 0)
                {
                    Finish();
                    yield break;
                }

                yield return new WaitForSeconds(1f);

                if (_isCountDown) CurrentTime--;
                else CurrentTime++;
            }
        }

        private void Finish()
        {
            IsRunning = false;
            _onComplete?.Invoke();
        }
    }
}