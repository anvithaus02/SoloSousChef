using UnityEngine;
using UnityEngine.UI;
using TMPro;
using com.SoloSousChef.UI.Generic;
namespace com.SoloSousChef.UI.Components
{
    [RequireComponent(typeof(CanvasGroup))]
    public class TimerProgressBar : MonoBehaviour
    {
        public enum DisplayMode { Increasing, Decreasing }

        [Header("UI References")]
        [SerializeField] private Image fillImage;
        [SerializeField] private TextMeshProUGUI label;

        private CanvasGroup _canvasGroup;
        private TickTimer _internalTimer;

        private void Awake()
        {
            _canvasGroup = GetComponent<CanvasGroup>();
            SetVisibility(false);
        }
        public void StartProgress(float duration, DisplayMode mode, System.Action onComplete = null)
        {
            StopProgress();
            SetVisibility(true);

            _internalTimer = new TickTimer(this, (int)duration, mode == DisplayMode.Decreasing,
                (currentTime) => UpdateUI(currentTime, duration),
                () =>
                {
                    SetVisibility(false);
                    onComplete?.Invoke();
                }
            );
        }
        private void UpdateUI(float current, float max)
        {
            fillImage.fillAmount = current / max;
            label.text = Mathf.CeilToInt(current).ToString();
        }

        public void StopProgress()
        {
            if (_internalTimer != null)
            {
                _internalTimer.Stop(false);
            }
            SetVisibility(false);
        }

        public void SetVisibility(bool isVisible)
        {
            if (_canvasGroup == null) _canvasGroup = GetComponent<CanvasGroup>();
            _canvasGroup.alpha = isVisible ? 1f : 0f;
            _canvasGroup.blocksRaycasts = isVisible;
        }
    }
}